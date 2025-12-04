import { useContext } from "react";
import AttributeCreateFormView from "./views/attributeCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { AttributeService } from "../../../services/attributeService";
import { IAttributeCreateDTO } from "../../../dto/attribute/IAttributeCreateDTO";

const AttributeCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new AttributeService();

    const initialValues = {
        attributeName: ''
    }

    const validate = (values: IAttributeCreateDTO) => {
        const errors = {} as IAttributeCreateDTO;

        if (!values.attributeName) {
            errors.attributeName = 'Required';
        } else if (values.attributeName.length > 64) {
            errors.attributeName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: IAttributeCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../attributes');
    }

    return (
        <AttributeCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default AttributeCreate;