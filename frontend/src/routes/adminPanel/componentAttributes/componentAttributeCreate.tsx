import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import ComponentAttributeCreateFormView from "./views/componentAttributeCreateFormView";
import { IComponentAttributeCreateDTO } from "../../../dto/componentAttribute/IComponentAttributeCreateDTO";
import { ComponentAttributeService } from "../../../services/componentAttributeService";
import { AttributeService } from "../../../services/attributeService";
import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";

const ComponentAttributeCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const { componentId } = useParams();
    const navigate = useNavigate();
    const componentAttributeService = new ComponentAttributeService();
    const attributeService = new AttributeService();

    const [selectData, setData] = useState([] as IAttributeDTO[]);

    useEffect(() => {
        fetchSelectData();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const attributeData = await attributeService.getAll();
        if (attributeData) setData(attributeData)
    };

    const initialValues = {
        componentId: componentId!,
        attributeId: "",
        attributeValue: ""
    }

    const validate = (values: IComponentAttributeCreateDTO) => {
        const errors = {} as IComponentAttributeCreateDTO;

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

    const onSubmit = async (values: IComponentAttributeCreateDTO) => {
        if (!jwtData) return;

        let response = await componentAttributeService.create(values, jwtData);
        if (response) navigate('../components/' + componentId);
    }

    return (
        <ComponentAttributeCreateFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ComponentAttributeCreate;