import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { PaymentService } from "../../../services/paymentService";
import { IPaymentEditDTO } from "../../../dto/payment/IPaymentEditDTO";
import PaymentEditFormView from "./views/paymentEditFormView";

const PaymentEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new PaymentService();
    const [initialValues, setInitialValues] = useState({id: "", comment: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id || !jwtData) return;

        let payment = await service.getEntityEdit(id, jwtData);

        if (payment) setInitialValues({
            id: id,
            comment: payment.comment
        });
    }

    const validate = (values: IPaymentEditDTO): IPaymentEditDTO => {
        const errors = {} as IPaymentEditDTO;

        if (!values.comment) {
            errors.comment = "Required";
        } else if (values.comment.length > 2048) {
            errors.comment = "Must be 2048 characters or less";
        }

        return errors;
    }

    const onSubmit = async (values: IPaymentEditDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../payments');
    }

    return (
        <PaymentEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default PaymentEdit;