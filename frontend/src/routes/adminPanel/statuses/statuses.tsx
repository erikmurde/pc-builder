import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { StatusService } from "../../../services/statusService";
import { useParams } from "react-router-dom";
import StatusDetails from "./statusDetails";
import FormHeader from "../../../components/form/FormHeader";
import { IStatusDTO } from "../../../dto/status/IStatusDTO";
import StatusListItem from "../../../components/entity/statuses/StatusListItem";

const Statuses = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new StatusService();
    const [data, setData] = useState([] as IStatusDTO[]);

    useEffect(() => {  
        getAll(); 
    }, [jwtData]);

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await service.delete(id, jwtData);
        if (response) getAll();
    }

    const getAll = async () => {
        let response = await service.getAll();
        setData(response ? response : []);
    }

    if (id) {
        return (
            <StatusDetails onDelete={onDelete}/>
        );
    }

    let statuses = [];
    for (let index = 0; index < data.length; index++) {
        statuses.push(<StatusListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Statuses" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    Status Name
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {statuses}
        </div>
    );
}

export default Statuses;