import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IPaymentSimpleDTO } from "../../../dto/payment/IPaymentSimpleDTO";

const PaymentSimpleListItem = (props: IBaseItemProps<IPaymentSimpleDTO>) => {
    return (
        <div className={"row image-entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-4 col-xl-5">
                {props.entity.paymentNr}
            </div>
            <div className="col-4 col-xl-3">
                {"$" + props.entity.amountPaid}
            </div>
            <div className="col-4">
                {new Date(props.entity.paymentDate).toUTCString()}
            </div>
        </div>
    );
}

export default PaymentSimpleListItem;