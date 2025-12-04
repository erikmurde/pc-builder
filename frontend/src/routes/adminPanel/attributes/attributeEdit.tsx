import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { AttributeService } from "../../../services/attributeService";
import AttributeEditFormView from "./views/attributeEditFormView";
import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";

const AttributeEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new AttributeService();
    const [initialValues, setInitialValues] = useState({id: "", attributeName: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let attribute = await service.getEntity(id);

        if (attribute) setInitialValues({
            id: id,
            attributeName: attribute.attributeName
        })
    }

    const validate = (values: IAttributeDTO): IAttributeDTO => {
        const errors = {} as IAttributeDTO;

        if (!values.attributeName) {
            errors.attributeName = 'Required';
        } else if (values.attributeName.length > 64) {
            errors.attributeName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: IAttributeDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../attributes');
    }

    return (
        <AttributeEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default AttributeEdit;