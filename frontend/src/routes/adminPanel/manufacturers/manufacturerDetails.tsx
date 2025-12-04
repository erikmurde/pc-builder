import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import { ManufacturerService } from "../../../services/manufacturerService";
import FormHeader from "../../../components/form/FormHeader";
import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";
import Manufacturer from "../../../components/entity/manufacturers/Manufacturer";

const ManufacturerDetails = (props: {onDelete: (id: string) => void}) => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new ManufacturerService();
    const [data, setData] = useState({} as IManufacturerDTO);

    useEffect(() => {
        fetchManufacturer();
    }, [jwtData]);

    const fetchManufacturer = async() => {
        if (!id) return;

        let manufacturer = await service.getEntity(id);
        if (manufacturer) setData(manufacturer);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manufacturer Details" nav="../manufacturers" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../manufacturers/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="manufacturer" nav="manufacturers" onDelete={props.onDelete}/>
                </div>
            </div>
            <Manufacturer entity={data}/>
        </div>
    );
}

export default ManufacturerDetails;