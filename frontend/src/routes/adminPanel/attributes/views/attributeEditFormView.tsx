import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IAttributeDTO } from "../../../../dto/attribute/IAttributeDTO";
import TableHead from "../../../../components/table/TableHead";

const AttributeEditFormView = (props: {
    initialValues: IAttributeDTO,
    validate: (values: IAttributeDTO) => IAttributeDTO,
    onSubmit: (values: IAttributeDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Attribute" nav="../attributes" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="attributeName" label="Attribute Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default AttributeEditFormView;