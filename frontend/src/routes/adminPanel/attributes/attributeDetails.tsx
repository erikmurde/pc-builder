import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { AttributeService } from "../../../services/attributeService";
import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";
import Attribute from "../../../components/entity/attributes/Attribute";

const AttributeDetails = (props: {onDelete: (id: string) => void}) => {
    const { id } = useParams();
    const service = new AttributeService();
    const [data, setData] = useState({} as IAttributeDTO);

    useEffect(() => {
        fetchAttribute();
    }, [id]);

    const fetchAttribute = async() => {
        if (!id) return;

        let attribute = await service.getEntity(id);
        if (attribute) setData(attribute);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Attribute Details" nav="../attributes" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../attributes/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="attribute" nav="attributes" onDelete={props.onDelete}/>
                </div>
            </div>
            <Attribute entity={data}/>
        </div>
    );
}

export default AttributeDetails;