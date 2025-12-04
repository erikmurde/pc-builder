import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import { ICategoryDTO } from "../../../../dto/category/ICategoryDTO";
import { IDiscountDTO } from "../../../../dto/discount/IDiscountDTO";
import { IManufacturerDTO } from "../../../../dto/manufacturer/IManufacturerDTO";
import FormSelectInput from "../../../../components/form/FormSelectInput";
import FormTextInput from "../../../../components/form/FormTextInput";
import FormTextAreaInput from "../../../../components/form/FormTextAreaInput";
import { IComponentEditDTO } from "../../../../dto/component/IComponentEditDTO";

const ComponentCreateFormView = (props: {
    initialValues: IComponentEditDTO,
    selectValues: {
        categories: ICategoryDTO[], 
        discounts: IDiscountDTO[], 
        manufacturers: IManufacturerDTO[]
    },
    validate: (values: IComponentEditDTO) => IComponentEditDTO,
    onSubmit: (values: IComponentEditDTO) => void
    }) => {

    let categorySelect = props.selectValues.categories
        .filter(c => !["Template PC", "Prebuilt PC", "Custom PC"].includes(c.categoryName))
        .map(c => ({name: c.categoryName, value: c.id}));

    let discountSelect = props.selectValues.discounts
        .map(d => ({name: d.discountName, value: d.id}));

    let manufacturerSelect = props.selectValues.manufacturers
        .map(m => ({name: m.manufacturerName, value: m.id}));

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Component" nav="../components" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="componentName" label="Name of Component" component={FormTextInput}/>
                        <Field name="imageSrc" label="Source of Image" component={FormTextInput}/>
                    </div>
                    <div className="row">
                        <Field name="categoryId" label="Category" component={FormSelectInput}
                        selectValues={categorySelect}/>
                    </div>
                    <div className="row">
                        <Field name="discountId" label="Discount" component={FormSelectInput}
                        selectValues={discountSelect}/>
                    </div>
                    <div className="row">
                        <Field name="manufacturerId" label="Manufacturer" component={FormSelectInput}
                        selectValues={manufacturerSelect}/>
                    </div>
                    <div className="row">
                        <Field name="description" label="Description" component={FormTextAreaInput}/>
                    </div>
                    <div className="row">
                        <Field type="number" name="price" label="Price (€)" component={FormTextInput}/>
                        <Field type="number" name="stock" label="Stock" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ComponentCreateFormView;