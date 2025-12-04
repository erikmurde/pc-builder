import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { ManufacturerService } from "../../../services/manufacturerService";
import ManufacturerCreateFormView from "./views/manufacturerCreateFormView";
import { IManufacturerCreateDTO } from "../../../dto/manufacturer/IManufacturerCreateDTO";

const ManufacturerCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new ManufacturerService();

    const initialValues = {
        manufacturerName: '',
    }

    const validate = (values: IManufacturerCreateDTO) => {
        const errors = {} as IManufacturerCreateDTO;

        if (!values.manufacturerName) {
            errors.manufacturerName = 'Required';
        } else if (values.manufacturerName.length > 128) {
            errors.manufacturerName = 'Must be 128 characters or less';
        }

        return errors;
    }

    const onSubmit = async (values: IManufacturerCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../manufacturers');
    }

    return (
        <ManufacturerCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default ManufacturerCreate;