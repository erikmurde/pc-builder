import { IManufacturerDTO } from "../../../dto/manufacturer/IManufacturerDTO";
import EntityProperty from "../../table/EntityProperty";

const Manufacturer = (props: {entity: IManufacturerDTO}) => {
    return (
        <EntityProperty name="Manufacturer Name" value={props.entity.manufacturerName} isEven={true}/>
    );
}

export default Manufacturer;