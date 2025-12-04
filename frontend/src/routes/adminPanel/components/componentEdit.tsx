import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { ComponentService } from "../../../services/componentService";
import { ManufacturerService } from "../../../services/manufacturerService";
import { CategoryService } from "../../../services/categoryService";
import { DiscountService } from "../../../services/discountService";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";
import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";
import ComponentEditFormView from "./views/componentEditFormView";
import { IComponentEditDTO } from "../../../dto/component/IComponentEditDTO";

const ComponentEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const componentService = new ComponentService();
    const categoryService = new CategoryService();
    const discountService = new DiscountService();
    const manufacturerService = new ManufacturerService();

    const [selectData, setData] = useState({
        categories: [] as ICategoryDTO[],
        discounts: [] as IDiscountDTO[],
        manufacturers: [] as IManufacturerDTO[]
    });

    const [initialValues, setInitialValues] = useState({
        id: "",
        categoryId: "",
        discountId: "",
        manufacturerId: "",
        componentName: "",
        description: "",
        price: "",
        stock: "",
        imageSrc: ""   
    });

    useEffect(() => {
        fetchSelectData();
        fetchInitialValues();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const categoryData = await categoryService.getAll();
        const discountData = await discountService.getAll();
        const manufacturerData = await manufacturerService.getAll();

        if (categoryData && discountData && manufacturerData) {
            setData({categories: categoryData, discounts: discountData, manufacturers: manufacturerData})
        }
    };

    const fetchInitialValues = async() => {
        if (!id) return;

        let component = await componentService.getEntityEdit(id);

        if (component) setInitialValues({
            id: id,
            componentName: component.componentName,
            description: component.description,
            price: component.price,
            stock: component.stock,
            imageSrc: component.imageSrc ?? "",
            categoryId: component.categoryId,
            discountId: component.discountId,
            manufacturerId: component.manufacturerId
        })
    }


    const validate = (values: IComponentEditDTO) => {
        const errors = {} as IComponentEditDTO;

        if (!values.categoryId) {
            errors.categoryId = "Required";
        }
        if (!values.discountId) {
            errors.discountId = "Required";
        }
        if (!values.manufacturerId) {
            errors.manufacturerId = "Required";
        }
        if (!values.componentName) {
            errors.componentName = "Required";
        } else if (values.componentName.length > 128) {
            errors.componentName = "Must be 128 characters or less";
        }
        if (!values.description) {
            errors.description = "Required";
        } else if (values.description.length > 512) {
            errors.description = "Must be 512 characters or less";
        }
        if (values.imageSrc && values.imageSrc.length > 256) {
            errors.imageSrc = "Must be 256 characters or less"; 
        }
        if (values.stock === "") {
            errors.stock = "Required";
        } else if (parseInt(values.stock) < 0 || parseInt(values.stock) > 1000) {
            errors.stock = "Must be between 0 and 1000";
        }
        if (!values.price) {
            values.price = "Required"
        } else if (parseInt(values.price) < 0 || parseInt(values.price) >= 100000) {
            errors.price = "Must be between 0 and 99,999.99";
        } else if (!/^\d+(\.\d{1,2})?$/.test(values.price)) {
            errors.price = "Must have up to 2 decimal places";
        }

        return errors;
    }

    const onSubmit = async (values: IComponentEditDTO) => {
        if (!jwtData || !id) return;

        if (!values.imageSrc) values.imageSrc = undefined;

        let response = await componentService.edit(id, values, jwtData);
        if (response) navigate('../components');
    }

    return (
        <ComponentEditFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ComponentEdit;