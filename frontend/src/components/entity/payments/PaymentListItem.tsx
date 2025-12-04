import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IPaymentDTO } from "../../../dto/payment/IPaymentDTO";

const PaymentListItem = (props: IBaseItemProps<IPaymentDTO>) => {
    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? " row-odd" : " row-even")}>
            <div className="col-3 col-lg-2">
                {props.entity.paymentNr}
            </div>
            <div className="col-4 col-lg-2">
                {props.entity.userEmail}
            </div>
            <div className="col-4 d-none d-lg-block">
                {new Date(props.entity.paymentDate).toUTCString()}
            </div>
            <div className="col-3 col-lg-2">
                {"$" + props.entity.amountPaid}
            </div>
            <div className="col-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
            </div>
        </div>
    );
}

export default PaymentListItem;