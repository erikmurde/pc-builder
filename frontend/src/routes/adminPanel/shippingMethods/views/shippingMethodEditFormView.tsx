import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IShippingMethodDTO } from "../../../../dto/shippingMethod/IShippingMethodDTO";
import TableHead from "../../../../components/table/TableHead";

const ShippingMethodEditFormView = (props: {
    initialValues: IShippingMethodDTO,
    validate: (values: IShippingMethodDTO) => IShippingMethodDTO,
    onSubmit: (values: IShippingMethodDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Shipping Method" nav="../shippingMethods" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
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

export default ShippingMethodEditFormView;