import { Field, FormikProps } from "formik";
import FormTextInput from "../form/FormTextInput";
import { ICheckoutFormValues } from "../../routes/checkout/CheckoutView";
import { IShippingMethodDTO } from "../../dto/shippingMethod/IShippingMethodDTO";
import { IShippingCostDTO } from "../../dto/shippingCost/IShippingCostDTO";
import FormSelectInput from "../form/FormSelectInput";

interface IProps {
    form: FormikProps<ICheckoutFormValues>,
    selectValues: {
        shippingMethods: IShippingMethodDTO[],
        shippingCosts: IShippingCostDTO[]
    }
}

const ShippingFields = (props: IProps) => {
    let shippingMethodValues = props.selectValues.shippingMethods
        .map(s => ({name: `${s.methodName} - ${s.shippingTime}`, value: s.id}));

    return (
        <>
            <div className="row">
                <Field name="streetAddress" label="Street Address" lengthLg={6} component={FormTextInput}/>
                <Field name="city" label="City" lengthLg={6} component={FormTextInput}/>
            </div>
            <div className="row">   
                <Field name="zipCode" label="Zip Code" lengthLg={6} component={FormTextInput}/>
                <Field name="shippingMethodId" label="Shipping Method" 
                selectValues={shippingMethodValues} lengthLg={6} component={FormSelectInput}/>
            </div>
        </>
    );
}

export default ShippingFields;