import { IPaymentDetailsDTO } from "../../../dto/payment/IPaymentDetailsDTO";
import EntityProperty from "../../table/EntityProperty";

const Payment = (props: {entity: IPaymentDetailsDTO}) => {
    return (
        <>
            <EntityProperty name="Payment Nr" value={props.entity.paymentNr} isEven={true}/>
            <EntityProperty name="Paid By" value={`${props.entity.customerName} (email ${props.entity.userEmail})`} isEven={false}/>
            <EntityProperty name="Order Nr" value={props.entity.orderNr} isEven={true}/>
            <EntityProperty name="Payment Date" value={new Date(props.entity.paymentDate).toUTCString()} isEven={false}/>
            <EntityProperty name="Amount Paid" value={"€" + Math.round(props.entity.amountPaid * 100) / 100} isEven={true}/>
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

export default Payment;