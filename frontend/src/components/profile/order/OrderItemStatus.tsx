import { IOrderDTO } from "../../../dto/order/IOrderDTO";

interface IProps {
    entity: IOrderDTO,
    lg?: boolean
}

const OrderItemStatus = (props: IProps) => {
    let color = "text-primary";

    if (props.entity.status === "Completed" && props.entity.orderCompletedAt !== null) {
        color = "text-success";
    } 
    else if (props.entity.status === "Cancelled" && props.entity.orderCancelledAt !== null) {
        color = "text-danger";
    } 

    if (props.lg) {
        return (
            <div className="col-3 text-end d-none d-lg-block">
                {props.entity.status}
                <i className={"fas fa-circle ms-2 " + (color)}></i>
            </div>
        );
    }

    return (
        <div className="row text-start d-lg-none mb-1">
            <div className="col">
                <i className={"fas fa-circle me-2 " + (color)}></i>
                {props.entity.status}
            </div>
        </div>
    );
}

export default OrderItemStatus;