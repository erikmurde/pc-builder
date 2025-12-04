import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import { IAttributeDTO } from "../../../../dto/attribute/IAttributeDTO";
import FormSelectInput from "../../../../components/form/FormSelectInput";
import { IComponentAttributeEditDTO } from "../../../../dto/componentAttribute/IComponentAttributeEditDTO";

const ComponentAttributeEditFormView = (props: {
    initialValues: IComponentAttributeEditDTO,
    selectValues: IAttributeDTO[],
    validate: (values: IComponentAttributeEditDTO) => IComponentAttributeEditDTO,
    onSubmit: (values: IComponentAttributeEditDTO) => void
    }) => {

    let attributeSelect = props.selectValues
        .map(a => ({name: a.attributeName, value: a.id}));

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Attribute of Component" nav={"../components/" + props.initialValues.componentId} btn="back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="attributeId" label="Attribute" component={FormSelectInput}
                        selectValues={attributeSelect}/>
                    </div>
                    <div className="row">
                        <Field name="attributeValue" label="Value" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ComponentAttributeEditFormView;