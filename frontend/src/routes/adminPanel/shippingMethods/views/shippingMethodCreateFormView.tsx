import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IShippingMethodCreateDTO } from "../../../../dto/shippingMethod/IShippingMethodCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const ShippingMethodCreateFormView = (props: {
    initialValues: IShippingMethodCreateDTO,
    validate: (values: IShippingMethodCreateDTO) => IShippingMethodCreateDTO,
    onSubmit: (values: IShippingMethodCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Shipping Method" nav="../shippingMethods" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="methodName" label="Method Name" component={FormTextInput}/>
                    </div>
                    <div className="row">
                        <Field name="shippingTime" label="Shipping Time" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ShippingMethodCreateFormView;