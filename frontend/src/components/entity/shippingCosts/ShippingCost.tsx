import { IShippingCostDTO } from "../../../dto/shippingCost/IShippingCostDTO";
import EntityProperty from "../../table/EntityProperty";

const shippingCost = (props: {entity: IShippingCostDTO}) => {
    return (
        <>
            <EntityProperty name="Package Size" value={props.entity.packageSize} isEven={true}/>
            <EntityProperty name="Shipping Method" value={props.entity.shippingMethod} isEven={false}/>
            <EntityProperty name="Cost Per Unit" value={"€" + Math.round(parseFloat(props.entity.costPerUnit) * 100) / 100} isEven={true}/>
        </>
    );
}

export default shippingCost;