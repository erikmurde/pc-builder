import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IOrderDTO } from "../../../dto/order/IOrderDTO";

const OrderListItem = (props: IBaseItemProps<IOrderDTO>) => {
    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-3 col-xl-2">
                {props.entity.orderNr}
            </div>
            <div className="col-3 col-xl-2">
                {props.entity.userEmail}
            </div>
            <div className="col-2 col-xxl-3 d-none d-xl-block">
                {new Date(props.entity.orderPlacedAt).toUTCString()}
            </div>
            <div className="col-2">
                {props.entity.status}
            </div>
            <div className="col-2">
                {"$" + Math.round(
                    (props.entity.totalPcCost + props.entity.totalShippingCost) 
                    * (1 - props.entity.discountPercentage / 100) * 100) / 100}
            </div>
            <div className="col-2 col-xxl-1">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
            </div>
        </div>
    );
}

export default OrderListItem;