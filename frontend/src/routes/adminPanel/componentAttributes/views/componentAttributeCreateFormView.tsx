import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import { IComponentAttributeCreateDTO } from "../../../../dto/componentAttribute/IComponentAttributeCreateDTO";
import { IAttributeDTO } from "../../../../dto/attribute/IAttributeDTO";
import FormSelectInput from "../../../../components/form/FormSelectInput";

const ComponentAttributeCreateFormView = (props: {
    initialValues: IComponentAttributeCreateDTO,
    selectValues: IAttributeDTO[],
    validate: (values: IComponentAttributeCreateDTO) => IComponentAttributeCreateDTO,
    onSubmit: (values: IComponentAttributeCreateDTO) => void
    }) => {

    let attributeSelect = props.selectValues
        .map(a => ({name: a.attributeName, value: a.id}));

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Add Attribute to Component" nav={"../components/" + props.initialValues.componentId} btn="back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
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

export default ComponentAttributeCreateFormView;