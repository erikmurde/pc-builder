import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { ICategoryDTO } from "../../../../dto/category/ICategoryDTO";
import TableHead from "../../../../components/table/TableHead";

const CategoryEditFormView = (props: {
    initialValues: ICategoryDTO,
    validate: (values: ICategoryDTO) => ICategoryDTO,
    onSubmit: (values: ICategoryDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Category" nav="../categories" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="categoryName" label="Category Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default CategoryEditFormView;