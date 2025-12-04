import { IOrderDetailsDTO } from "../../../dto/order/IOrderDetailsDTO";
import EntityProperty from "../../table/EntityProperty";

const Order = (props: {entity: IOrderDetailsDTO}) => {
    let pcCost = props.entity.orderPcs 
        ? props.entity.orderPcs.reduce((sum, o) => sum += o.pricePerUnit * o.qty, 0)
        : 0;

    let completedDate = props.entity.orderCompletedAt !== null
        ? <EntityProperty 
            name="Completed At" 
            value={new Date(props.entity.orderCompletedAt).toUTCString()} 
            isEven={false}/>
        : <></>;
        
    let cancelledDate = props.entity.orderCancelledAt !== null
        ? <EntityProperty 
            name="Cancelled At" 
            value={new Date(props.entity.orderCancelledAt).toUTCString()} 
            isEven={false}/>
        : <></>;
    
    let isCancelledOrCompleted = 
        props.entity.orderCancelledAt !== null || 
        props.entity.orderCompletedAt !== null;

    return (
        <>
            <EntityProperty name="Order Nr" value={props.entity.orderNr} isEven={true}/>
            <EntityProperty name="Placed By" value={props.entity.userEmail} isEven={false}/>
            <EntityProperty name="Placed At" value={new Date(props.entity.orderPlacedAt).toUTCString()} isEven={true}/>
            {completedDate}
            {cancelledDate}
            <EntityProperty name="Status" value={props.entity.status} isEven={isCancelledOrCompleted}/>
            <EntityProperty name="Customer Name" value={props.entity.customerName} isEven={!isCancelledOrCompleted}/>
            <EntityProperty name="Customer Phone Number" value={props.entity.customerPhoneNumber} isEven={isCancelledOrCompleted}/>
            <EntityProperty name="Shipping Address" value={props.entity.shippingAddress} isEven={!isCancelledOrCompleted}/>
            <EntityProperty name="Postal Code" value={props.entity.shippingPostalCode} isEven={isCancelledOrCompleted}/>
            <EntityProperty name="Shipping Method" value={props.entity.shippingMethod} isEven={!isCancelledOrCompleted}/>
            <EntityProperty name="Total Cost of Shipping" value={"€" + props.entity.totalShippingCost} isEven={isCancelledOrCompleted}/>
            <EntityProperty 
                name="Total Cost of PCs" 
                value={"$" + Math.round(pcCost * 100) / 100} 
                isEven={!isCancelledOrCompleted}/>
            <EntityProperty 
                name="Total Cost Before Discount" 
                value={"$" + Math.round((pcCost + props.entity.totalShippingCost) * 100) / 100} 
                isEven={isCancelledOrCompleted}/>
            <EntityProperty name="Discount Name" value={props.entity.discountName} isEven={!isCancelledOrCompleted}/>
            <EntityProperty name="Discount" value={props.entity.discountPercentage + "%"} isEven={isCancelledOrCompleted}/>
            <EntityProperty 
                name="Final Cost" 
                value={"$" + Math.round((pcCost + props.entity.totalShippingCost) * (1 - props.entity.discountPercentage / 100) * 100) / 100} 
                isEven={!isCancelledOrCompleted}/>
            <br/>
            <div className="row table-head">
                <div className="col-12">
                    Comment
                </div>
            </div>
            <br/>
            <EntityProperty value={props.entity.comment ? props.entity.comment : "No Comment"} isEven={false}/>
            <br/>
        </>
    );
}

export default Order;