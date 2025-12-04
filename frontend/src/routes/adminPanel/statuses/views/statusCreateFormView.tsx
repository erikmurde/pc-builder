import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IStatusCreateDTO } from "../../../../dto/status/IStatusCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const StatusCreateFormView = (props: {
    initialValues: IStatusCreateDTO,
    validate: (values: IStatusCreateDTO) => IStatusCreateDTO,
    onSubmit: (values: IStatusCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Status" nav="../statuses" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="statusName" label="Status Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default StatusCreateFormView;