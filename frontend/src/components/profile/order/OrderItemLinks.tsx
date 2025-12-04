import { Link } from "react-router-dom";
import OrderCancelModal from "../../modal/OrderCancelModal";
import { IOrderEditDTO } from "../../../dto/order/IOrderEditDTO";
import { IOrderDTO } from "../../../dto/order/IOrderDTO";

interface IProps {
    entity: IOrderDTO,
    statusId: string,
    lg?: boolean

    onSubmit: (values: IOrderEditDTO) => void
}

const OrderItemLinks = (props: IProps) => {
    let isActive = !["Cancelled", "Completed"].includes(props.entity.status)
    let cancelLink = <></>;

    if (isActive) {
        cancelLink = props.lg 
        ?
        <div className="row d-none d-lg-flex">
            <div className="col">
                <OrderCancelModal id={props.entity.id} statusId={props.statusId} onSubmit={props.onSubmit}/>
            </div>
        </div>
        : 
        <div className="col-7 d-lg-none p-0">
            <div className="col">
                <OrderCancelModal id={props.entity.id} statusId={props.statusId} onSubmit={props.onSubmit}/>
            </div>
        </div>
    }
    
    if (props.lg) {        
        return (
            <>
                <div className="row mb-1 d-none d-lg-flex">
                    <div className="col">
                        <Link to={props.entity.id} className="text-decoration-none">view details</Link>
                    </div>
                </div>
                {cancelLink}
            </>
        );
    }
    
    return (
        <>
            <div className={"col-5 d-lg-none"}>
                <div className="col">
                    <Link to={props.entity.id} className="text-decoration-none">view details</Link>
                </div>
            </div>
            {cancelLink}
        </>
    );
}

export default OrderItemLinks;