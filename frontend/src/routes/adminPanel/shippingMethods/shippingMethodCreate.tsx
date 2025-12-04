import { useContext } from "react";
import ShippingMethodCreateFormView from "./views/shippingMethodCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import { IShippingMethodCreateDTO } from "../../../dto/shippingMethod/IShippingMethodCreateDTO";

const ShippingMethodCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new ShippingMethodService();

    const initialValues = {
        methodName: "",
        shippingTime: ""
    }

    const validate = (values: IShippingMethodCreateDTO) => {
        const errors = {} as IShippingMethodCreateDTO;

        if (!values.methodName) {
            errors.methodName = "Required";
        } else if (values.methodName.length > 64) {
            errors.methodName = "Must be 64 characters or less";
        }
        if (!values.shippingTime) {
            errors.shippingTime = "Required";
        } else if (values.shippingTime.length > 64) {
            errors.shippingTime = "Must be 64 characters or less";
        }

        return errors;
    }

    const onSubmit = async (values: IShippingMethodCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../shippingMethods');
    }

    return (
        <ShippingMethodCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default ShippingMethodCreate;