import FormTextInput from "../../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import { ICategoryCreateDTO } from "../../../../dto/category/ICategoryCreateDTO";
import TableHead from "../../../../components/table/TableHead";

const CategoryCreateFormView = (props: {
    initialValues: ICategoryCreateDTO,
    validate: (values: ICategoryCreateDTO) => ICategoryCreateDTO,
    onSubmit: (values: ICategoryCreateDTO) => void
    }) => {

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Create Category" nav="../categories" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}>
                <Form>
                    <TableHead title="Properties" btnName="Create"/>
                    <div className="row mt-3">
                        <Field name="categoryName" label="Category Name" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default CategoryCreateFormView;