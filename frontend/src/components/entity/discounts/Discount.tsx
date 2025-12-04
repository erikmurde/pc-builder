import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";
import EntityProperty from "../../table/EntityProperty";

const Discount = (props: {entity: IDiscountDTO}) => {
    return (
        <>
            <EntityProperty name="Discount Name" value={props.entity.discountName} isEven={true}/>
            <EntityProperty name="Discount Amount" value={props.entity.discountPercentage + "%"} isEven={false}/>
        </>
    );
}

export default Discount;