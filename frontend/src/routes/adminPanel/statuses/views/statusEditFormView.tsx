import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IStatusDTO } from "../../../../dto/status/IStatusDTO";
import TableHead from "../../../../components/table/TableHead";

const StatusEditFormView = (props: {
    initialValues: IStatusDTO,
    validate: (values: IStatusDTO) => IStatusDTO,
    onSubmit: (values: IStatusDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Status" nav="../statuses" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="statusName" label="Status Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default StatusEditFormView;