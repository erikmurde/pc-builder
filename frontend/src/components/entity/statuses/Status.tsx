import { IStatusDTO } from "../../../dto/status/IStatusDTO";
import EntityProperty from "../../table/EntityProperty";

const Status = (props: {entity: IStatusDTO}) => {
    return (
        <EntityProperty name="Status Name" value={props.entity.statusName} isEven={true}/>
    );
}

export default Status;