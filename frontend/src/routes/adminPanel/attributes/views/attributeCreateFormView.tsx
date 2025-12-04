import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IAttributeCreateDTO } from "../../../../dto/attribute/IAttributeCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const AttributeCreateFormView = (props: {
    initialValues: IAttributeCreateDTO,
    validate: (values: IAttributeCreateDTO) => IAttributeCreateDTO,
    onSubmit: (values: IAttributeCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Attribute" nav="../attributes" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="attributeName" label="Attribute Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default AttributeCreateFormView;