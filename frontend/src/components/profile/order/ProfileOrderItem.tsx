import { IOrderDTO } from "../../../dto/order/IOrderDTO";
import { IOrderEditDTO } from "../../../dto/order/IOrderEditDTO";
import OrderItemStatus from "../order/OrderItemStatus";
import OrderItemLinks from "../order/OrderItemLinks";

interface IProps {
    entity: IOrderDTO,
    statusId: string,
    
    onSubmit: (values: IOrderEditDTO) => void
}

const ProfileOrderItem = (props: IProps) => {
    let isActive = false;
    let daterow: JSX.Element = <></>;

    let cost = (props.entity.totalPcCost + props.entity.totalShippingCost) 
        * (1 - props.entity.discountPercentage / 100);

    if (props.entity.status === "Completed" && props.entity.orderCompletedAt !== null) {
        daterow = 
        <div className="row mb-1">
            Completed {new Date(props.entity.orderCompletedAt).toDateString()}
        </div>
    } 
    else if (props.entity.status === "Cancelled" && props.entity.orderCancelledAt !== null) {
        daterow = 
        <div className="row mb-1">
            Cancelled {new Date(props.entity.orderCancelledAt).toDateString()}
        </div>
    } else isActive = true;

    return (
        <div className="row profile-order shadow-sm flex-center p-2 mb-2">
            <div className="col-5 col-lg-4">
                <div className="row mb-1">
                    <strong>{props.entity.orderNr}</strong> 
                </div>
                <OrderItemLinks entity={props.entity} statusId={props.statusId} onSubmit={props.onSubmit} lg/>
                <OrderItemStatus entity={props.entity}/>
            </div>
            <div className="col-7 col-lg-5">
                <div className="row mb-1">
                    Placed {new Date(props.entity.orderPlacedAt).toDateString()}
                </div>
                {daterow}
                <div className={"row" + (isActive ? "" : " d-none d-lg-flex")}>
                    ${Math.round(cost * 100) / 100}
                </div>
            </div>
            <OrderItemLinks entity={props.entity} statusId={props.statusId} onSubmit={props.onSubmit}/>
            <OrderItemStatus entity={props.entity} lg/>
            <div className={"col-7 d-lg-none p-0" + (isActive ? " d-none" : "")}>
                ${Math.round(cost * 100) / 100}
            </div>
        </div>
    );
}

export default ProfileOrderItem;