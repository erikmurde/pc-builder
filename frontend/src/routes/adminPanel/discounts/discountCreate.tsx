import { useContext } from "react";
import DiscountCreateFormView from "./views/discountCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { DiscountService } from "../../../services/discountService";
import { IDiscountCreateDTO } from "../../../dto/discount/IDiscountCreateDTO";

const DiscountCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new DiscountService();

    const initialValues = {
        discountName: '',
        discountPercentage: ''
    }

    const validate = (values: IDiscountCreateDTO) => {
        const errors = {} as IDiscountCreateDTO;

        if (!values.discountName) {
            errors.discountName = 'Required';
        } else if (values.discountName.length > 128) {
            errors.discountName = 'Must be 128 characters or less';
        }
        if (values.discountPercentage === "") {
            errors.discountPercentage = 'Required';
        } else if (parseInt(values.discountPercentage) < 0 || parseInt(values.discountPercentage) > 100) {
            errors.discountPercentage = 'Must be between 0 and 100';
        }

        return errors;
    }

    const onSubmit = async (values: IDiscountCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../discounts');
    }

    return (
        <DiscountCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default DiscountCreate;