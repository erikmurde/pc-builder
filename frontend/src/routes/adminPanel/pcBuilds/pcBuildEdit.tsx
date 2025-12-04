import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { PcBuildService } from "../../../services/pcBuildService";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import { CategoryService } from "../../../services/categoryService";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";
import { IComponentSimpleDTO } from "../../../dto/component/IComponentSimpleDTO";
import { DiscountService } from "../../../services/discountService";
import { ComponentService } from "../../../services/componentService";
import PcBuildEditFormView from "./views/pcBuildEditFormView";
import { IPcBuildEditDTO } from "../../../dto/pcBuild/IPcBuildEditDTO";

const PcBuildEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const pcBuildService = new PcBuildService();
    const categoryService = new CategoryService();
    const discountService = new DiscountService();
    const componentService = new ComponentService();

    const [selectData, setData] = useState({
        categories: [] as ICategoryDTO[],
        discounts: [] as IDiscountDTO[],
        components: [] as IComponentSimpleDTO[]
    });
    
    const [initialValues, setInitialValues] = useState({
        id: "",
        pcName: "",
        description: "",
        stock: "",
        imageSrc: "",
        categoryId: "",
        discountId: "",
        caseId: "",
        motherboardId: "",
        processorId: "",
        cpuCoolerId: "",
        memoryId: "",
        graphicsCardId: "",
        primaryStorageId: "",
        powerSupplyId: "",
        operatingSystemId: "",
        secondaryStorageId: ""
    });

    useEffect(() => {
        fetchSelectData();
        fetchInitialValues();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const categoryData = await categoryService.getAll();
        const discountData = await discountService.getAll();
        const componentData = await componentService.getAllSimple();

        if (categoryData && discountData && componentData) {
            setData({categories: categoryData, discounts: discountData, components: componentData})
        } 
    };

    const fetchInitialValues = async() => {
        if (!id || !jwtData) return;

        let pcBuild = await pcBuildService.getEntityEdit(id, jwtData);

        if (pcBuild) setInitialValues({
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
            secondaryStorageId: pcBuild.secondaryStorageId ?? ""
        })
    }

    const validate = (values: IPcBuildEditDTO) => {
        const errors = {} as IPcBuildEditDTO;

        if (!values.pcName) {
            errors.pcName = "Required";
        } else if (values.pcName.length > 64) {
            errors.pcName = "Must be 64 characters or less";
        }
        if (values.imageSrc && values.imageSrc.length > 256) {
            errors.imageSrc = "Must be 256 characters or less";
        }
        if (!values.description) {
            errors.description = "Required";
        } else if (values.description.length > 512) {
            errors.description = "Must be 512 characters or less";
        }
        if (values.stock === "") {
            errors.stock = "Required";
        } else if (parseInt(values.stock) < 0 || parseInt(values.stock) > 1000) {
            errors.stock = "Must be between 0 and 1000";
        }
        if (!values.categoryId) {
            errors.categoryId = "Required";
        } 
        if (!values.discountId) {
            errors.discountId = "Required";
        }
        if (!values.caseId) {
            errors.caseId = "Required";
        }
        if (!values.motherboardId) {
            errors.motherboardId = "Required";
        }
        if (!values.processorId) {
            errors.processorId = "Required";
        }
        if (!values.cpuCoolerId) {
            errors.cpuCoolerId = "Required";
        }
        if (!values.memoryId) {
            errors.memoryId = "Required";
        }
        if (!values.graphicsCardId) {
            errors.graphicsCardId = "Required";
        }
        if (!values.primaryStorageId) {
            errors.primaryStorageId = "Required";
        }
        if (!values.secondaryStorageId) {
            errors.secondaryStorageId = "Required";
        }
        if (!values.powerSupplyId) {
            errors.powerSupplyId = "Required";
        }
        if (!values.operatingSystemId) {
            errors.operatingSystemId = "Required";
        }

        return errors;
    }

    const onSubmit = async (values: IPcBuildEditDTO) => {
        if (!id || !jwtData) return;

        if (!values.imageSrc) values.imageSrc = undefined;
        if (values.secondaryStorageId === "None") values.secondaryStorageId = undefined;

        let response = await pcBuildService.edit(id, values, jwtData);
        if (response) navigate('../pcBuilds');
    }

    return (
        <PcBuildEditFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default PcBuildEdit;