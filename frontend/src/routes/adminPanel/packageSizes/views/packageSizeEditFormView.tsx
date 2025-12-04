import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { IPackageSizeDTO } from "../../../../dto/packageSize/IPackageSizeDTO";
import TableHead from "../../../../components/table/TableHead";

const PackageSizeEditFormView = (props: {
    initialValues: IPackageSizeDTO,
    validate: (values: IPackageSizeDTO) => IPackageSizeDTO,
    onSubmit: (values: IPackageSizeDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Package Size" nav="../packageSizes" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
            <Form>
                <TableHead title="Properties" btnName="Edit"/>
                <div className="row mt-3">
                    <Field name="sizeName" label="Size Name" component={FormTextInput}/>
                </div>
            </Form>
            </Formik>
        </div>
    );
}

export default PackageSizeEditFormView;