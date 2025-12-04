import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { StatusService } from "../../../services/statusService";
import StatusEditFormView from "./views/statusEditFormView";
import { IStatusDTO } from "../../../dto/status/IStatusDTO";

const StatusEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new StatusService();
    const [initialValues, setInitialValues] = useState({id: "", statusName: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let status = await service.getEntity(id);

        if (status) setInitialValues({
            id: id,
            statusName: status.statusName
        })
    }

    const validate = (values: IStatusDTO): IStatusDTO => {
        const errors = {} as IStatusDTO;

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

    const onSubmit = async (values: IStatusDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../statuses');
    }

    return (
        <StatusEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default StatusEdit;