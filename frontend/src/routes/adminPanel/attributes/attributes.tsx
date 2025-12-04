import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";
import { AttributeService } from "../../../services/attributeService";
import AttributeDetails from "./attributeDetails";
import AttributeListItem from "../../../components/entity/attributes/AttributeListItem";


const Attributes = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new AttributeService();
    const [data, setData] = useState([] as IAttributeDTO[]);

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
            <AttributeDetails onDelete={onDelete}/>
        );
    }

    let attributes = [];
    for (let index = 0; index < data.length; index++) {
        attributes.push(<AttributeListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Attributes" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    Attribute Name
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {attributes}
        </div>
    );
}

export default Attributes;