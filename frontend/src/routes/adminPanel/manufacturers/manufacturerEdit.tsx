import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { ManufacturerService } from "../../../services/manufacturerService";
import ManufacturerEditFormView from "./views/manufacturerEditFormView";
import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";

const ManufacturerEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new ManufacturerService();
    const [initialValues, setInitialValues] = useState({id: "", manufacturerName: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let manufacturer = await service.getEntity(id);

        if (manufacturer) setInitialValues({
            id: id,
            manufacturerName: manufacturer.manufacturerName
        })
    }

    const validate = (values: IManufacturerDTO): IManufacturerDTO => {
        const errors = {} as IManufacturerDTO;

        if (!values.manufacturerName) {
            errors.manufacturerName = 'Required';
        } else if (values.manufacturerName.length > 128) {
            errors.manufacturerName = 'Must be 128 characters or less';
        }

        return errors;
    }

    const onSubmit = async (values: IManufacturerDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../manufacturers');
    }

    return (
        <ManufacturerEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default ManufacturerEdit;