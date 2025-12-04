import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { DiscountService } from "../../../services/discountService";
import DiscountEditFormView from "./views/discountEditFormView";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";

const DiscountEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new DiscountService();
    const [initialValues, setInitialValues] = useState({
        id: "", 
        discountName: "", 
        discountPercentage: ""
    });

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let discount = await service.getEntity(id);

        if (discount) setInitialValues({
            id: id,
            discountName: discount.discountName,
            discountPercentage: discount.discountPercentage 
        })
    }

    const validate = (values: IDiscountDTO): IDiscountDTO => {
        const errors = {} as IDiscountDTO;

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

    const onSubmit = async (values: IDiscountDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../discounts');
    }

    return (
        <DiscountEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default DiscountEdit;