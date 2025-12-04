import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useNavigate, useParams } from "react-router-dom";
import { IdentityService } from "../../../services/identityService";
import { PaymentService } from "../../../services/paymentService";
import { IPaymentDTO } from "../../../dto/payment/IPaymentDTO";
import { IPaymentEditDTO } from "../../../dto/payment/IPaymentEditDTO";
import ProfilePaymentItem from "../../../components/profile/payment/ProfilePaymentItem";
import PaymentHistoryView from "./views/PaymentHistoryView";
import UserPaymentDetails from "./UserPaymentDetails";

const PaymentHistory = () => {
    const paymentService = new PaymentService();
    const identityService = new IdentityService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const [data, setData] = useState([] as IPaymentDTO[]);

    useEffect(() => { 
        if (!jwtData) return navigate("../login");
        getAll(); 
    }, [jwtData]);

    const getAll = async () => {
        let response = await paymentService.getAll(jwtData!);

        if (response) {
            let jwtObject = identityService.getJwtObject(jwtData!);
            setData(response.filter(p => p.userEmail === jwtObject.email));
        }
    }

    const onSubmit = async(values: IPaymentEditDTO) => {
        if (!jwtData) return;

        console.log(values);

        let response = await paymentService.edit(values.id, values, jwtData);
        if (response) getAll();
    }

    let payments: JSX.Element[] = [];

    data.forEach(payment => {
        payments.push(<ProfilePaymentItem key={payment.id} entity={payment} onSubmit={onSubmit}/>);
    });

    if (id) {
        return <UserPaymentDetails />;
    }

    return (
        <PaymentHistoryView payments={payments}/>
    );
}

export default PaymentHistory;