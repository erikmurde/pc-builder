import { useContext } from "react";
import StatusCreateFormView from "./views/statusCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { StatusService } from "../../../services/statusService";
import { IStatusCreateDTO } from "../../../dto/status/IStatusCreateDTO";

const StatusCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new StatusService();

    const initialValues = {
        statusName: ''
    }

    const validate = (values: IStatusCreateDTO) => {
        const errors = {} as IStatusCreateDTO;

        if (!values.statusName) {
            errors.statusName = "Required";
        } else if (values.statusName.length > 64) {
            errors.statusName = "Must be 64 characters or less";
        }
        if (values.comment && values.comment.length > 2048) {
            errors.comment = "Must be 2048 characters or less";
        }
        
        return errors;
    }

    const onSubmit = async (values: IStatusCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../statuses');
    }

    return (
        <StatusCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default StatusCreate;