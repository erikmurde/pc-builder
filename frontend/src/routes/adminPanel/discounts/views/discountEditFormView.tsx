import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IDiscountDTO } from "../../../../dto/discount/IDiscountDTO";
import TableHead from "../../../../components/table/TableHead";

const DiscountEditFormView = (props: {
    initialValues: IDiscountDTO,
    validate: (values: IDiscountDTO) => IDiscountDTO,
    onSubmit: (values: IDiscountDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Discount" nav="../discounts" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="discountName" label="Discount Name" component={FormTextInput}/>
                    </div>
                    <div className="row">
                        <Field name="discountPercentage" label="Discount Percentage" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default DiscountEditFormView;