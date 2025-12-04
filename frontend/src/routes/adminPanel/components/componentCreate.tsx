import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import ComponentCreateFormView from "./views/componentCreateFormView";
import { IComponentCreateDTO } from "../../../dto/component/IComponentCreateDTO";
import { ComponentService } from "../../../services/componentService";
import { ManufacturerService } from "../../../services/manufacturerService";
import { CategoryService } from "../../../services/categoryService";
import { DiscountService } from "../../../services/discountService";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";
import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";

const ComponentCreate = () => {
    const { jwtData } = useContext(JwtContext);
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

    useEffect(() => {
        fetchSelectData();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const categoryData = await categoryService.getAll();
        const discountData = await discountService.getAll();
        const manufacturerData = await manufacturerService.getAll();

        if (categoryData && discountData && manufacturerData) {
            setData({categories: categoryData, discounts: discountData, manufacturers: manufacturerData})
        }
    };

    const initialValues = {
        categoryId: "",
        discountId: "",
        manufacturerId: "",
        componentName: "",
        description: "",
        price: "",
        stock: "",
        imageSrc: ""   
    }

    const validate = (values: IComponentCreateDTO) => {
        const errors = {} as IComponentCreateDTO;

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

    const onSubmit = async (values: IComponentCreateDTO) => {
        if (!jwtData) return;

        if (!values.imageSrc) values.imageSrc = undefined;

        let response = await componentService.create(values, jwtData);
        if (response) navigate('../components');
    }

    return (
        <ComponentCreateFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ComponentCreate;