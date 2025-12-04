import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IPackageSizeCreateDTO } from "../../../../dto/packageSize/IPackageSizeCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const PackageSizeCreateFormView = (props: {
    initialValues: IPackageSizeCreateDTO,
    validate: (values: IPackageSizeCreateDTO) => IPackageSizeCreateDTO,
    onSubmit: (values: IPackageSizeCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Package Size" nav="../packageSizes" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="sizeName" label="Size Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default PackageSizeCreateFormView;