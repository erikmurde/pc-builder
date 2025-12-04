import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IDiscountCreateDTO } from "../../../../dto/discount/IDiscountCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const DiscountCreateFormView = (props: {
    initialValues: IDiscountCreateDTO,
    validate: (values: IDiscountCreateDTO) => IDiscountCreateDTO,
    onSubmit: (values: IDiscountCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Discount" nav="../discounts" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
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

export default DiscountCreateFormView;