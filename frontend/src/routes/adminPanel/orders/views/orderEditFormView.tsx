import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import FormSelectInput from "../../../../components/form/FormSelectInput";
import FormTextAreaInput from "../../../../components/form/FormTextAreaInput";
import { IOrderEditDTO } from "../../../../dto/order/IOrderEditDTO";
import { IStatusDTO } from "../../../../dto/status/IStatusDTO";

const OrderEditFormView = (props: {
    initialValues: IOrderEditDTO,
    selectValues: IStatusDTO[],
    validate: (values: IOrderEditDTO) => IOrderEditDTO,
    onSubmit: (values: IOrderEditDTO) => void
    }) => {

    let statusSelect = props.selectValues
        .map(c => ({name: c.statusName, value: c.id}));

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Order Status" nav="../orders" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="statusId" label="status" component={FormSelectInput}
                        selectValues={statusSelect}/>
                    </div>
                    <div className="row">
                        <Field name="comment" label="Comment" component={FormTextAreaInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default OrderEditFormView;