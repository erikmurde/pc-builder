import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import EntityProperty from "../../table/EntityProperty";

const packageSize = (props: {entity: IPackageSizeDTO}) => {
    return (
        <EntityProperty name="Size Name" value={props.entity.sizeName} isEven={true}/>
    );
}

export default packageSize;