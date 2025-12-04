import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { CategoryService } from "../../../services/categoryService";
import CategoryEditFormView from "./views/categoryEditFormView";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";

const CategoryEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new CategoryService();
    const [initialValues, setInitialValues] = useState({id: "", categoryName: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let category = await service.getEntity(id);

        if (category) setInitialValues({
            id: id,
            categoryName: category.categoryName
        })
    }

    const validate = (values: ICategoryDTO): ICategoryDTO => {
        const errors = {} as ICategoryDTO;

        if (!values.categoryName) {
            errors.categoryName = 'Required';
        } else if (values.categoryName.length > 64) {
            errors.categoryName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: ICategoryDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../categories');
    }

    return (
        <CategoryEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default CategoryEdit;