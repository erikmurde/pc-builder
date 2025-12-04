import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IManufacturerCreateDTO } from "../../../../dto/manufacturer/IManufacturerCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const ManufacturerCreateFormView = (props: {
    initialValues: IManufacturerCreateDTO,
    validate: (values: IManufacturerCreateDTO) => IManufacturerCreateDTO,
    onSubmit: (values: IManufacturerCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Manufacturer" nav="../manufacturers" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="manufacturerName" label="Manufacturer Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ManufacturerCreateFormView;