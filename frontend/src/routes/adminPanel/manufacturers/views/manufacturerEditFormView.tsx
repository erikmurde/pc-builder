import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IManufacturerDTO } from "../../../../dto/manufacturer/IManufacturerDTO";
import TableHead from "../../../../components/table/TableHead";

const ManufacturerEditFormView = (props: {
    initialValues: IManufacturerDTO,
    validate: (values: IManufacturerDTO) => IManufacturerDTO,
    onSubmit: (values: IManufacturerDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Manufacturer" nav="../manufacturers" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="manufacturerName" label="Manufacturer Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ManufacturerEditFormView;