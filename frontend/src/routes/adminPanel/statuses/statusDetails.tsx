import { useEffect, useState } from "react";
import { StatusService } from "../../../services/statusService";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { IStatusDTO } from "../../../dto/status/IStatusDTO";
import Status from "../../../components/entity/statuses/Status";

const StatusDetails = (props: {onDelete: (id: string) => void}) => {
    const { id } = useParams();
    const service = new StatusService();
    const [data, setData] = useState({} as IStatusDTO);

    useEffect(() => {
        fetchStatus();
    }, [id]);

    const fetchStatus = async() => {
        if (!id) return;

        let status = await service.getEntity(id);
        if (status) setData(status);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Status Details" nav="../statuses" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../statuses/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="status" nav="statuses" onDelete={props.onDelete}/>
                </div>
            </div>
            <Status entity={data}/>
        </div>
    );
}

export default StatusDetails;