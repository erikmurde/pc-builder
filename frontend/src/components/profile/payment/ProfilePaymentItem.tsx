import { IPaymentDTO } from "../../../dto/payment/IPaymentDTO";
import { IPaymentEditDTO } from "../../../dto/payment/IPaymentEditDTO";
import PaymentItemStatus from "../payment/PaymentItemStatus";
import PaymentItemLinks from "../payment/PaymentItemLinks";

interface IProps {
    entity: IPaymentDTO,

    onSubmit: (values: IPaymentEditDTO) => void
}

const ProfilePaymentItem = (props: IProps) => {
    return (
        <div className="row profile-order shadow-sm flex-center p-2 mb-2">
            <div className="col-5 col-lg-4">
                <div className="row mb-1">
                    <strong>{props.entity.paymentNr}</strong> 
                </div>
                <PaymentItemLinks entity={props.entity} onSubmit={props.onSubmit} lg/>
                <PaymentItemStatus />
            </div>
            <div className="col-7 col-lg-5">
                <div className="row mb-1">
                    Order {props.entity.orderNr}
                </div>
                <div className="row mb-1">
                    Date {new Date(props.entity.paymentDate).toDateString()}
                </div>
                <div className="row">
                    Amount ${Math.round(props.entity.amountPaid * 100) / 100}
                </div>
            </div>
            <PaymentItemLinks entity={props.entity} onSubmit={props.onSubmit}/>
            <PaymentItemStatus lg/>
        </div>
    );
}

export default ProfilePaymentItem;