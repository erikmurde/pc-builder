import { Field, Form, Formik } from "formik";
import OrderSummary from "../../components/checkout/OrderSummary";
import FormHeader from "../../components/form/FormHeader";
import { IShippingMethodDTO } from "../../dto/shippingMethod/IShippingMethodDTO";
import { IShippingCostDTO } from "../../dto/shippingCost/IShippingCostDTO";
import CustomerFields from "../../components/checkout/CustomerFields";
import ShippingFields from "../../components/checkout/ShippingFields";
import FormTextAreaInput from "../../components/form/FormTextAreaInput";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import { IPackageSizeDTO } from "../../dto/packageSize/IPackageSizeDTO";

export interface ICheckoutFormValues {
    shippingMethodId: string,
    firstName: string,
    lastName: string,
    email: string,
    phoneNumber: string,
    streetAddress: string,
    city: string,
    zipCode: string,
    comment: string,
}

interface IProps {
    initialValues: ICheckoutFormValues,
    cartPcs: ICartPcDTO[],
    selectValues: {
        shippingMethods: IShippingMethodDTO[],
        shippingCosts: IShippingCostDTO[],
        packageSizes: IPackageSizeDTO[]
    }

    validate: (values: ICheckoutFormValues) => void,
    onSubmit: (values: ICheckoutFormValues, setStatus: (status: any) => void) => void
}

const CheckoutView = (props: IProps) => {
    return (
        <Formik
            initialValues={props.initialValues}
            validate={(values) => props.validate(values)}
            onSubmit={(values, { setStatus }) => props.onSubmit(values, setStatus)}
            enableReinitialize>
            {(form => <Form>
                <div className="row justify-content-center m-0">
                    <div className="col-10 col-xl-7 content-panel">
                        <FormHeader title="Information" center/>
                        <div className="row mt-2 p-2">
                            <h5>Customer</h5>
                            <hr className="config-hr"/>
                        </div>
                        <CustomerFields form={form}/>
                        <div className="row mt-2 p-2">
                            <h5>Shipping Address</h5>
                            <hr className="config-hr"/>
                        </div>
                        <ShippingFields form={form} selectValues={props.selectValues}/>
                        <div className="row mt-2 p-2">
                            <h5>Order Notes (optional)</h5>
                            <hr className="config-hr"/>
                        </div>
                        <div className="row">
                            <Field name="comment" label="Notes about your order" 
                            length={12} component={FormTextAreaInput}/>
                        </div>
                    </div>
                    <div className="col-12 col-xl flex-grow-1"></div>
                    <OrderSummary form={form} cartPcs={props.cartPcs} selectValues={props.selectValues}/>
                </div>
            </Form>)}
        </Formik>
    );
}

export default CheckoutView;