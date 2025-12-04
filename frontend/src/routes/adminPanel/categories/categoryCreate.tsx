import { useContext } from "react";
import CategoryCreateFormView from "./views/categoryCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { CategoryService } from "../../../services/categoryService";
import { ICategoryCreateDTO } from "../../../dto/category/ICategoryCreateDTO";

const CategoryCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new CategoryService();

    const initialValues = {
        categoryName: ''
    }

    const validate = (values: ICategoryCreateDTO) => {
        const errors = {} as ICategoryCreateDTO;

        if (!values.categoryName) {
            errors.categoryName = 'Required';
        } else if (values.categoryName.length > 64) {
            errors.categoryName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: ICategoryCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../categories');
    }

    return (
        <CategoryCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default CategoryCreate;