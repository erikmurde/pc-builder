import { Link } from "react-router-dom";
import { IPaymentDTO } from "../../../dto/payment/IPaymentDTO";
import { IPaymentEditDTO } from "../../../dto/payment/IPaymentEditDTO";
import PaymentModal from "../../modal/PaymentModal";

interface IProps {
    entity: IPaymentDTO,
    lg?: boolean

    onSubmit: (values: IPaymentEditDTO) => void
}

const PaymentItemLinks = (props: IProps) => {
    if (props.lg) {        
        return (
            <>
                <div className="row mb-1 d-none d-lg-flex">
                    <div className="col">
                        <Link to={props.entity.id} className="text-decoration-none">view details</Link>
                    </div>
                </div>
                <div className="row d-none d-lg-flex">
                    <div className="col">
                        <PaymentModal id={props.entity.id} onSubmit={props.onSubmit}/>
                    </div>
                </div>
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
            <div className="col-7 d-lg-none p-0">
                <div className="col">
                    <PaymentModal id={props.entity.id} onSubmit={props.onSubmit}/>
                </div>
            </div>
        </>
    );
}

export default PaymentItemLinks;