import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { ComponentService } from "../../../services/componentService";
import { IComponentDetailsDTO } from "../../../dto/component/IComponentDetailsDTO";
import { ComponentAttributeService } from "../../../services/componentAttributeService";
import EntityProperty from "../../../components/table/EntityProperty";
import ComponentAttributeListItem from "../../../components/entity/componentAttributes/ComponentAttributeListItem";
import Component from "../../../components/entity/components/Component";

const ComponentDetails = (props: {onDelete: (id: string) => void}) => {
    const componentService = new ComponentService();
    const componentAttributeService = new ComponentAttributeService();
    const { jwtData } = useContext(JwtContext);
    const [data, setData] = useState({} as IComponentDetailsDTO);
    const { id } = useParams();

    useEffect(() => {
        fetchComponent();
    }, [jwtData]);

    const onAttributeDelete = async(attributeId: string) => {
        if (!jwtData || !id) return;

        let response = await componentAttributeService.delete(attributeId, jwtData);
        if (response) fetchComponent();
    }

    const fetchComponent = async() => {
        if (!id) return;

        let component = await componentService.getEntity(id);
        if (component) setData(component);
    }

    let componentAttributes = [];
    if (data.componentAttributes) {
        for (let index = 0; index < data.componentAttributes.length; index++) {
            componentAttributes.push(<ComponentAttributeListItem 
                key={index} 
                index={index} 
                entity={data.componentAttributes[index]} 
                onDelete={onAttributeDelete}/>);
        }
    }

    let noAttributesText = componentAttributes.length === 0
    ?   <>
            <br/><EntityProperty value="Component has no attributes" isEven={false}/><br/>
        </>      
    : <></>      

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Component Details" nav="../components" btn="Back"/>
            <div className="row table-head">
                <div className="col-8">
                    General Details
                </div>
                <div className="col-2 text-center">
                    <Link to={"../components/edit/" + id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="component" nav="components" onDelete={props.onDelete}/>
                </div>
                <div className="col-2 p-0">
                    <Link to={"../componentAttributes/create/" + id}>
                        <button className="btn btn-success btn-crud">
                            <i className="fa-solid fa-plus text-white me-0 me-lg-2"></i>
                            <span className="d-none d-lg-inline">Attribute</span>
                        </button>
                    </Link>
                </div>
            </div>
            <Component entity={data}/>
            <div className="row table-head">
                <div className="col-4">
                    AttributeName
                </div>
                <div className="col-6">
                    AttributeValue
                </div>
                <div className="col-2">
                    Actions
                </div>
            </div>
            {noAttributesText}
            {componentAttributes}
        </div>
    );
}

export default ComponentDetails;