import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { PaymentService } from "../../../services/paymentService";
import { IPaymentDetailsDTO } from "../../../dto/payment/IPaymentDetailsDTO";
import Payment from "../../../components/entity/payments/Payment";

const PaymentDetails = () => {
    const service = new PaymentService();
    const { jwtData } = useContext(JwtContext);
    const [data, setData] = useState({} as IPaymentDetailsDTO);
    const { id } = useParams();

    useEffect(() => {
        fetchPayment();
    }, [jwtData]);

    const fetchPayment = async() => {
        if (!id || !jwtData) return;

        let payment = await service.getEntity(id, jwtData);
        if (payment) setData(payment);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Payment Details" nav="../payments" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../payments/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                </div>
            </div>
            <Payment entity={data}/>
        </div>
    );
}

export default PaymentDetails;