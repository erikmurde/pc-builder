import { Field, Form, Formik } from "formik";
import FormHeader from "../../../../components/form/FormHeader";
import FormSelectInput from "../../../../components/form/FormSelectInput";
import { ICategoryDTO } from "../../../../dto/category/ICategoryDTO";
import { IDiscountDTO } from "../../../../dto/discount/IDiscountDTO";
import { IComponentSimpleDTO } from "../../../../dto/component/IComponentSimpleDTO";
import TableHead from "../../../../components/table/TableHead";
import FormTextAreaInput from "../../../../components/form/FormTextAreaInput";
import FormTextInput from "../../../../components/form/FormTextInput";
import { IPcBuildEditDTO } from "../../../../dto/pcBuild/IPcBuildEditDTO";

const PcBuildEditFormView = (props: {
    initialValues: IPcBuildEditDTO,
    selectValues: {
        categories: ICategoryDTO[], 
        discounts: IDiscountDTO[], 
        components: IComponentSimpleDTO[]
    },
    validate: (values: IPcBuildEditDTO) => IPcBuildEditDTO,
    onSubmit: (values: IPcBuildEditDTO) => void
    }) => {

    const getComponentValues = (category: string) => {
        return props.selectValues.components
            .filter(c => c.categoryName == category)
            .map(d => ({name: d.componentName, value: d.id}))
    }

    let categorySelect = props.selectValues.categories
        .filter(c => ["Template PC", "Prebuilt PC"].includes(c.categoryName))
        .map(c => ({name: c.categoryName, value: c.id}));

    let discountSelect = props.selectValues.discounts
        .map(d => ({name: d.discountName, value: d.id}));

    let caseSelect = getComponentValues("Case");
    let motherboardSelect = getComponentValues("Motherboard");
    let cpuSelect = getComponentValues("Processor");
    let cpuCoolerSelect = getComponentValues("CPU Cooler");
    let memorySelect = getComponentValues("Memory");
    let gpuSelect = getComponentValues("Graphics Card");
    let powerSupplySelect = getComponentValues("Power Supply");
    let osSelect = getComponentValues("Operating System");
    let primaryStorageSelect = getComponentValues("Solid State Drive");
    let secondaryStorageSelect = getComponentValues("Hard Drive");

    secondaryStorageSelect.push({name: "None", value: "None"})
    
    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Edit PC Build" nav="../pcbuilds" btn="Back"/>
            <Formik
                initialValues={props.initialValues}
                validate={(values) => props.validate(values)}
                onSubmit={(values) => props.onSubmit(values)}
                enableReinitialize={true}>
                <Form>
                    <TableHead title="General Info" btnName="Edit"/>
                    <div className="row mt-3">
                        <Field name="pcName" label="Name of PC" component={FormTextInput}/>
                        <Field name="imageSrc" label="Source of Image" component={FormTextInput}/>
                    </div>
                    <div className="row">              
                        <Field name="categoryId" label="Category" length={4} component={FormSelectInput}
                        selectValues={categorySelect}/>
                        <Field name="discountId" label="Discount" length={4} component={FormSelectInput}
                        selectValues={discountSelect}/>
                    </div>
                    <div className="row">
                        <Field name="description" label="Description" component={FormTextAreaInput}/>
                    </div>
                    <div className="row"> 
                        <Field type="number" name="stock" label="Stock" component={FormTextInput}/>
                    </div>
                    <TableHead title="Components"/>
                    <div className="row mt-3">
                        <Field name="caseId" label="Case" component={FormSelectInput} selectValues={caseSelect}/>
                    </div>
                    <div className="row">
                        <Field name="motherboardId" label="Motherboard" component={FormSelectInput} selectValues={motherboardSelect}/>
                    </div>
                    <div className="row">
                        <Field name="processorId" label="Processor" component={FormSelectInput} selectValues={cpuSelect}/>
                    </div>
                    <div className="row">
                        <Field name="cpuCoolerId" label="CPU Cooler" component={FormSelectInput} selectValues={cpuCoolerSelect}/>
                    </div>
                    <div className="row">
                        <Field name="memoryId" label="Memory" component={FormSelectInput} selectValues={memorySelect}/>
                    </div>
                    <div className="row">
                        <Field name="graphicsCardId" label="Graphics Card" component={FormSelectInput} selectValues={gpuSelect}/>
                    </div>
                    <div className="row">
                        <Field name="primaryStorageId" label="Primary Storage" component={FormSelectInput} selectValues={primaryStorageSelect}/>
                    </div>
                    <div className="row">
                        <Field name="secondaryStorageId" label="Secondary Storage" component={FormSelectInput} selectValues={secondaryStorageSelect}/>
                    </div>
                    <div className="row">
                        <Field name="powerSupplyId" label="Power Supply" component={FormSelectInput} selectValues={powerSupplySelect}/>
                    </div>
                    <div className="row">
                        <Field name="operatingSystemId" label="Operating System" component={FormSelectInput} selectValues={osSelect}/>
                    </div>
                </Form>
            </Formik>
        </div>
    );
}

export default PcBuildEditFormView;