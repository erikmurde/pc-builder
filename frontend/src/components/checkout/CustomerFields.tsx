import { Field, FormikProps } from "formik";
import FormTextInput from "../form/FormTextInput";
import { ICheckoutFormValues } from "../../routes/checkout/CheckoutView";

interface IProps {
    form: FormikProps<ICheckoutFormValues>
}

const CustomerFields = (props: IProps) => {
    return (
        <>
            <div className="row">
                <Field name="firstName" label="First Name" lengthLg={6} component={FormTextInput}/>
                <Field name="lastName" label="Last Name" lengthLg={6} component={FormTextInput}/>
            </div>
            <div className="row">
                <Field name="email" label="Email" lengthLg={6} component={FormTextInput}/>
                <Field name="phoneNumber" label="Phone Number" lengthLg={6} component={FormTextInput}/>
            </div>
        </>
    );
}

export default CustomerFields;