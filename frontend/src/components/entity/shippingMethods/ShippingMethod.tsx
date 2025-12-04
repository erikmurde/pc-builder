import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";
import EntityProperty from "../../table/EntityProperty";

const shippingMethod = (props: {entity: IShippingMethodDTO}) => {
    return (
        <>
            <EntityProperty name="Method Name" value={props.entity.methodName} isEven={true}/>
            <EntityProperty name="Shipping Time" value={props.entity.shippingTime} isEven={false}/>
        </>
    );
}

export default shippingMethod;