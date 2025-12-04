import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import ManufacturerDetails from "./manufacturerDetails";
import { ManufacturerService } from "../../../services/manufacturerService";
import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";
import ManufacturerListItem from "../../../components/entity/manufacturers/ManufacturerListItem";

const Manufacturers = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new ManufacturerService();
    const [data, setData] = useState([] as IManufacturerDTO[]);

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
            <ManufacturerDetails onDelete={onDelete}/>
        );
    }

    let manufacturers = [];
    for (let index = 0; index < data.length; index++) {
        manufacturers.push(<ManufacturerListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Manufacturers" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    Manufacturer Name
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {manufacturers}
        </div>
    );
}

export default Manufacturers;