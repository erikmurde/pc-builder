import { Field, Form, Formik, useFormik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import TableHead from "../../../../components/table/TableHead";
import FormSelectInput from "../../../../components/form/FormSelectInput";
import FormTextInput from "../../../../components/form/FormTextInput";
import { IPackageSizeDTO } from "../../../../dto/packageSize/IPackageSizeDTO";
import { IShippingMethodDTO } from "../../../../dto/shippingMethod/IShippingMethodDTO";
import { IShippingCostEditDTO } from "../../../../dto/shippingCost/IShippingCostEditDTO";

const ShippingCostEditFormView = (props: {
    initialValues: IShippingCostEditDTO,
    selectValues: {
        packageSizes: IPackageSizeDTO[], 
        shippingMethods: IShippingMethodDTO[]
    },
    validate: (values: IShippingCostEditDTO) => IShippingCostEditDTO,
    onSubmit: (values: IShippingCostEditDTO) => void
    }) => {

    let packageSizeSelect = props.selectValues.packageSizes
        .map(p => ({name: p.sizeName, value: p.id}));

    let shippingMethodSelect = props.selectValues.shippingMethods
        .map(s => ({name: s.methodName, value: s.id}));

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit Shipping Cost" nav="../shippingCosts" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="Properties" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="packageSizeId" label="Package Size" component={FormSelectInput}
                        selectValues={packageSizeSelect}/>
                    </div>
                    <div className="row">
                        <Field name="shippingMethodId" label="Shipping Method" component={FormSelectInput}
                        selectValues={shippingMethodSelect}/>
                    </div>
                    <div className="row">
                        <Field type="number" name="costPerUnit" label="Cost Per Unit (€)" component={FormTextInput}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default ShippingCostEditFormView;