import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import ShippingMethodEditFormView from "./views/shippingMethodEditFormView";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";

const ShippingMethodEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new ShippingMethodService();
    const [initialValues, setInitialValues] = useState({
        id: "", 
        methodName: "",
        shippingTime: ""
    });

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let shippingMethod = await service.getEntity(id);

        if (shippingMethod) setInitialValues({
            id: id,
            methodName: shippingMethod.methodName,
            shippingTime: shippingMethod.shippingTime
        })
    }

    const validate = (values: IShippingMethodDTO): IShippingMethodDTO => {
        const errors = {} as IShippingMethodDTO;

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

    const onSubmit = async (values: IShippingMethodDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../shippingMethods');
    }

    return (
        <ShippingMethodEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default ShippingMethodEdit;