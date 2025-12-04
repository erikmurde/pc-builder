import { IAttributeDTO } from "../../../dto/attribute/IAttributeDTO";
import EntityProperty from "../../table/EntityProperty";

const Attribute = (props: {entity: IAttributeDTO}) => {
    return (
        <EntityProperty name="Attribute Name" value={props.entity.attributeName} isEven={true}/>
    );
}

export default Attribute;