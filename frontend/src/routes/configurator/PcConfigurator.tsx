import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CartCountContext, JwtContext } from "../root";
import { ComponentService } from "../../services/componentService";
import { PcBuildService } from "../../services/pcBuildService";
import { IComponentDTO } from "../../dto/component/IComponentDTO";
import PcConfiguratorView from "./PcConfiguratorView";
import { IPcBuildCreateDTO } from "../../dto/pcBuild/IPcBuildCreateDTO";
import { CategoryService } from "../../services/categoryService";
import { CartPcService } from "../../services/cartPcService";
import { IPcBuildEditDTO } from "../../dto/pcBuild/IPcBuildEditDTO";
import LoadingFailed from "../../components/InternalError";
import { DiscountService } from "../../services/discountService";
import { IComponentDetailsDTO } from "../../dto/component/IComponentDetailsDTO";

const PcConfigurator = () => {
    const { jwtData } = useContext(JwtContext);
    const { cartCount, setCartCount } = useContext(CartCountContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const categoryService = new CategoryService();
    const componentService = new ComponentService();
    const pcBuildService = new PcBuildService();
    const cartPcService = new CartPcService();
    const discountService = new DiscountService();

    const [discount, setDiscount] = useState(0);
    const [components, setComponents] = useState([] as IComponentDTO[]);
    const [detailedComponents, setDetailedComponents] = useState([] as IComponentDetailsDTO[])
    const [initialValues, setInitialValues] = useState({} as IPcBuildEditDTO);

    useEffect(() => {  
        if (!jwtData || !id) return navigate("../login");
        
        fetchAllComponents().then(
            components => {
                if (!components) return <LoadingFailed message={"Failed to load configurator"}/>;
                fetchInitialValues().then(
                    values => {
                        if (!values) return <LoadingFailed message={"Failed to load configurator"}/>;

                        checkCategory(values);
                        checkCpuAndMotherboard(components, values);
                        fetchDiscount(values.discountId);
                    }
                )
            }
        )
        fetchSelectedComponents(id);
    }, []);

    // If pc category is invalid, kick the user out
    const checkCategory = async(initValues: IPcBuildEditDTO) => {
        let categories = await categoryService.getAll();

        if (categories) {
            let category = categories.filter(c => c.id === initValues.categoryId)[0];

            if (!["Custom PC", "Template PC"].includes(category.categoryName)) {
                return navigate("../");
            }
        };
    }

    // Removes invalid cpus and motherboards. This check is only done once
    const checkCpuAndMotherboard = async(components: IComponentDTO[], initValues: IPcBuildEditDTO) => {    
        let selectedCpu = components.filter(c => c.id == initValues.processorId)[0];

        // If selected cpu is intel, remove all amd cpus and vice versa
        let filtered = components.filter(c => !(
            c.categoryName === "Processor" && 
            c.manufacturerName !== selectedCpu.manufacturerName
        ));

        // Allowed motherboard sockets depending on selected cpu
        let allowedSockets = selectedCpu.manufacturerName === "Intel" 
            ? ["LGA1700"] 
            : ["AM4", "AM5"];

        let motherboards = await componentService.getAllMotherboard(jwtData!);
        if (!motherboards) return;
 
        // If selected cpu is intel, remove all amd motherboards and vice versa
        setComponents(filtered.filter(c => !(c.categoryName == "Motherboard" && !allowedSockets.includes(
            motherboards!.filter(m => 
                m.id === c.id)[0].componentAttributes.filter(a => 
                    a.attributeName === "Socket")[0].attributeValue
        ))));
    }

    const fetchAllComponents = async(): Promise<IComponentDTO[] | undefined> => {
        let response = await componentService.getAll();

        if (response) {
            setComponents(response.sort((a, b) => Number(a.price) > Number(b.price) ? 1 : -1));
            return response;
        } 
    }

    const fetchSelectedComponents = async(id: string) => {
        let response = await componentService.getAllSelected(id, jwtData!);
        if (response) setDetailedComponents(response);
    }

    const fetchDiscount = async(id: string) => {
        let response = await discountService.getEntity(id);
        if (response) setDiscount(Number(response.discountPercentage));
    }

    const fetchInitialValues = async(): Promise<IPcBuildEditDTO | undefined> => {
        if (!id) return;

        let pcBuild = await pcBuildService.getEntityEdit(id, jwtData!);

        if (!pcBuild) return;

        let values = {
            id: id,
            pcName: pcBuild.pcName,
            description: pcBuild.description,
            stock: pcBuild.stock,
            imageSrc: pcBuild.imageSrc ?? "",
            categoryId: pcBuild.categoryId,
            discountId: pcBuild.discountId,
            caseId: pcBuild.caseId,
            motherboardId: pcBuild.motherboardId,
            processorId: pcBuild.processorId,
            cpuCoolerId: pcBuild.cpuCoolerId,
            memoryId: pcBuild.memoryId,
            graphicsCardId: pcBuild.graphicsCardId,
            primaryStorageId: pcBuild.primaryStorageId,
            powerSupplyId: pcBuild.powerSupplyId,
            operatingSystemId: pcBuild.operatingSystemId,
            secondaryStorageId: pcBuild.secondaryStorageId ?? "None"
        }

        setInitialValues(values);
        return values;
    }

    const validate = async(values: IPcBuildCreateDTO) => {   
        // Fetch all currently selected components for compatibility checking
        let pcCase = await getSelectedComponent(values.caseId);
        let motherboard = await getSelectedComponent(values.motherboardId);
        let cpu = await getSelectedComponent(values.processorId);
        let cpuCooler = await getSelectedComponent(values.cpuCoolerId);
        let memory = await getSelectedComponent(values.memoryId);
        let gpu = await getSelectedComponent(values.graphicsCardId);
        let primaryStorage = await getSelectedComponent(values.primaryStorageId);
        let powerSupply = await getSelectedComponent(values.powerSupplyId);
        let secondaryStorage = values.secondaryStorageId && values.secondaryStorageId !== "None"
            ? await getSelectedComponent(values.secondaryStorageId)
            : undefined;

        const errors = {} as IPcBuildCreateDTO;

        // Check if components were loaded
        if (!pcCase || !motherboard || !cpu || !cpuCooler || 
            !memory || !gpu || !primaryStorage || !powerSupply) {
            errors.pcName = "Error fetching components";
            return errors;
        }

        // Check if any component is out of stock
        if (Number(pcCase.stock) === 0) errors.caseId = "Insufficient stock - Case";
        if (Number(motherboard.stock) === 0) errors.motherboardId = "Insufficient stock - Motherboard";
        if (Number(cpu.stock) === 0) errors.processorId = "Insufficient stock - CPU";
        if (Number(cpuCooler.stock) === 0) errors.cpuCoolerId = "Insufficient stock - CPU cooler";
        if (Number(memory.stock) === 0) errors.memoryId = "Insufficient stock - Memory";
        if (Number(gpu.stock) === 0) errors.graphicsCardId = "Insufficient stock - GPU";
        if (Number(primaryStorage.stock) === 0) errors.primaryStorageId = "Insufficient stock - Primary Storage";
        if (secondaryStorage && Number(secondaryStorage.stock) === 0) errors.secondaryStorageId = "Insufficient stock - Secondary Storage";
        if (Number(powerSupply.stock) === 0) errors.powerSupplyId = "Insufficient stock - Power Supply";
        
        let cpuSocket = cpu.componentAttributes.filter(c => c.attributeName === "Socket")[0];
        let motherboardSocket = motherboard.componentAttributes.filter(c => c.attributeName === "Socket")[0];

        // Check if cpu and motherboard are compatible
        if (cpuSocket.attributeValue !== motherboardSocket.attributeValue) {
            errors.processorId = "CPU and motherboard are incompatible"
        }

        let motherboardMemory = motherboard.componentAttributes.filter(c => c.attributeName === "Supported Memory")[0];
        let memoryType = memory.componentAttributes.filter(c => c.attributeName === "Memory Type")[0];

        // Check if ram and motherboard are compatible
        if (motherboardMemory.attributeValue !== memoryType.attributeValue) {
            errors.memoryId = `Motherboard only supports ${motherboardMemory.attributeValue} memory`
        }

        let supportedFormFactors = pcCase.componentAttributes.filter(c => c.attributeName === "Supported Motherboards")[0];
        let formFactor = motherboard.componentAttributes.filter(c => c.attributeName === "Form Factor")[0];

        // Check if case and motherboard are compatible
        if (!supportedFormFactors.attributeValue.includes(formFactor.attributeValue)) {
            errors.caseId = `Case does not support ${formFactor.attributeValue} motherboards`
        }

        return errors;
    }
    
    const getSelectedComponent = async(id: string): Promise<IComponentDetailsDTO | undefined> => {
        let component = detailedComponents.filter(c => c.id === id)[0];

        if (component) return component;

        let response = await componentService.getEntity(id);
        if (!response) return;

        setDetailedComponents(detailedComponents => [...detailedComponents, response!]);
        return response;
    }

    const onSubmit = async (values: IPcBuildCreateDTO) => {
        if (!jwtData) return navigate("../login");

        values.imageSrc = (components.filter(c => c.id === values.caseId)[0].imageSrc);

        if (values.secondaryStorageId === "None") values.secondaryStorageId = undefined;
        if (!values.imageSrc) values.imageSrc = undefined;

        let pcBuildResponse = undefined;
        let customCategory = await categoryService.getCustomCategory();

        // Set PC category to custom and create PC build
        if (customCategory) {
            pcBuildResponse = await pcBuildService
                .create({...values, categoryId: customCategory.id, stock: "0"}, jwtData);
        } 

        // Create cart PC with newly created PC build
        if (pcBuildResponse?.id) {
            let cartPc = await cartPcService.create({
                pcBuildId: pcBuildResponse.id,
                qty: 1
            }, jwtData)

            if (cartPc) {
                if (setCartCount) setCartCount(cartCount + 1);
                navigate("../cart");
            } 
        }
    }

    return (
        <PcConfiguratorView 
            initialValues={initialValues} 
            components={components} 
            pcDiscount={discount} 
            validate={validate} 
            onSubmit={onSubmit}/>
    )
}

export default PcConfigurator;