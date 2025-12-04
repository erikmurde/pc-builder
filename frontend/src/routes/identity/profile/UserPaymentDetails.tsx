import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useNavigate, useParams } from "react-router-dom";
import { PaymentService } from "../../../services/paymentService";
import { IPaymentDetailsDTO } from "../../../dto/payment/IPaymentDetailsDTO";
import Payment from "../../../components/entity/payments/Payment";

const UserPaymentDetails = () => {
    const service = new PaymentService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const [data, setData] = useState({} as IPaymentDetailsDTO);
    const { id } = useParams();

    useEffect(() => {
        if (!jwtData || !id) return navigate("../login");

        fetchPayment();
    }, [jwtData]);

    const fetchPayment = async() => {
        let payment = await service.getEntity(id!, jwtData!);
        if (payment) setData(payment);
    }

    return (
        <>
            <div className="row flex-center table-head m-0 p-2">
                <div className="col-12">
                    <h2>Payment Details</h2>
                </div>
            </div>
            <div className="row flex-center m-0 p-2">
                <div className="col-12">
                    <Payment entity={data}/>
                </div>
            </div>
        </>
    );
}

export default UserPaymentDetails;