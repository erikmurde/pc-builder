import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { ComponentAttributeService } from "../../../services/componentAttributeService";
import { AttributeService } from "../../../services/attributeService";
import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";
import { IComponentAttributeEditDTO } from "../../../dto/componentAttribute/IComponentAttributeEditDTO";
import ComponentAttributeEditFormView from "./views/componentAttributeEditFormView";

const ComponentAttributeEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const componentAttributeService = new ComponentAttributeService();
    const attributeService = new AttributeService();

    const [selectData, setData] = useState([] as IAttributeDTO[]);

    const [initialValues, setInitialValues] = useState({
        id: "",
        componentId: "",
        attributeId: "",
        attributeValue: ""
    });

    useEffect(() => {
        fetchSelectData();
        fetchInitialValues();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const attributeData = await attributeService.getAll();
        if (attributeData) setData(attributeData)
    };

    const fetchInitialValues = async() => {
        if (!jwtData || !id) return;

        let componentAttribute = await componentAttributeService.getEntityEdit(id, jwtData);

        if (componentAttribute) setInitialValues({
            id: id,
            componentId: componentAttribute.componentId,
            attributeId: componentAttribute.attributeId,
            attributeValue: componentAttribute.attributeValue
        })
    }

    const validate = (values: IComponentAttributeEditDTO) => {
        const errors = {} as IComponentAttributeEditDTO;

        if (!values.attributeId) {
            errors.attributeId = "Required";
        }
        if (!values.attributeValue) {
            errors.attributeValue = "Required";
        } else if (values.attributeValue.length > 128) {
            errors.attributeValue = "Must be 128 characters or less";
        }

        return errors;
    }

    const onSubmit = async (values: IComponentAttributeEditDTO) => {
        if (!jwtData || !id) return;

        let response = await componentAttributeService.edit(id, values, jwtData);
        if (response) navigate('../components/' + values.componentId);
    }

    return (
        <ComponentAttributeEditFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ComponentAttributeEdit;