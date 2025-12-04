import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import { IPaymentEditDTO } from "../../../../dto/payment/IPaymentEditDTO";
import FormTextAreaInput from "../../../../components/form/FormTextAreaInput";

const PaymentEditFormView = (props: {
    initialValues: IPaymentEditDTO,
    validate: (values: IPaymentEditDTO) => IPaymentEditDTO,
    onSubmit: (values: IPaymentEditDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Payment Comment" nav="../payments" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Property" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="comment" label="Comment" component={FormTextAreaInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default PaymentEditFormView;