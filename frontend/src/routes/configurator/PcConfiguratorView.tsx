import { Formik, Field, Form } from "formik";
import ConfiguratorSummaryCard from "../../components/configurator/ConfiguratorSummary";
import FormHeader from "../../components/form/FormHeader";
import { IComponentDTO } from "../../dto/component/IComponentDTO";
import { IPcBuildEditDTO } from "../../dto/pcBuild/IPcBuildEditDTO";
import { useState } from "react";
import { IPcBuildCreateDTO } from "../../dto/pcBuild/IPcBuildCreateDTO";
import TableHead from "../../components/table/TableHead";
import ComponentSelection from "../../components/configurator/ComponentSelection";

const PcConfiguratorView = (props: {
    initialValues: IPcBuildEditDTO,
    components: IComponentDTO[],
    pcDiscount: number,
    validate: (values: IPcBuildCreateDTO) => Promise<IPcBuildCreateDTO>,
    onSubmit: (values: IPcBuildCreateDTO) => void
    }) => {

    const [openId, setOpenId] = useState(0);

    const toggleOpen = (id: number) => {
        setOpenId(id === openId ? 0 : id);
    }

    let caseSelection = props.components
        .filter(c => c.categoryName === "Case");

    let motherboardSelection = props.components
        .filter(c => c.categoryName === "Motherboard");

    let cpuSelection = props.components
        .filter(c => c.categoryName === "Processor");

    let cpuCoolerSelection = props.components
        .filter(c => c.categoryName === "CPU Cooler");

    let memorySelection = props.components
        .filter(c => c.categoryName === "Memory");

    let gpuSelection = props.components
        .filter(c => c.categoryName === "Graphics Card");

    let primaryStorageSelection = props.components
        .filter(c => c.categoryName === "Solid State Drive");

    let secondaryStorageSelection = props.components
        .filter(c => c.categoryName === "Hard Drive");

    let powerSupplySelection = props.components
        .filter(c => c.categoryName === "Power Supply");

    let osSelection = props.components
        .filter(c => c.categoryName === "Operating System");

    secondaryStorageSelection.unshift({
        id: "None",
        categoryName: "Hard Drive",
        manufacturerName: "",
        discountPercentage: 0, 
        componentName: "None", 
        price: "0", 
        stock: 0,
        imageSrc: "https://www.cyberpowerpc.com/images/None_Selected_220.png"
    });

    return (
        <Formik    
            initialValues={props.initialValues}
            validate={(values) => props.validate(values)}
            onSubmit={(values) => props.onSubmit(values)}
            enableReinitialize={true}>
            {(form => <Form>
                <div className="row" id="config-container">
                    <div className="col-7 col-xl-4 p-0 d-flex justify-content-center">
                        <ConfiguratorSummaryCard components={props.components} form={form} pcDiscount={props.pcDiscount}/>
                    </div>
                    <div className="col-7 content-panel text-center shadow">
                        <FormHeader title={form.values["pcName"]} nav=""/>
                        <TableHead title="Select the parts you want in your PC"/>
                        <div id="accordion">
                            <Field open={openId === 1} onClick={() => toggleOpen(1)} component={ComponentSelection}
                            name="caseId" components={caseSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 2} onClick={() => toggleOpen(2)} component={ComponentSelection}
                            name="motherboardId" components={motherboardSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 3} onClick={() => toggleOpen(3)} component={ComponentSelection}
                            name="processorId" components={cpuSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 4} onClick={() => toggleOpen(4)} component={ComponentSelection}
                            name="cpuCoolerId" components={cpuCoolerSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 5} onClick={() => toggleOpen(5)} component={ComponentSelection}
                            name="memoryId" components={memorySelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 6} onClick={() => toggleOpen(6)} component={ComponentSelection}
                            name="graphicsCardId" components={gpuSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 7} onClick={() => toggleOpen(7)} component={ComponentSelection}
                            name="primaryStorageId" components={primaryStorageSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 8} onClick={() => toggleOpen(8)} component={ComponentSelection}
                            name="secondaryStorageId" components={secondaryStorageSelection} pcDiscount={props.pcDiscount}/>

                            <Field open={openId === 9} onClick={() => toggleOpen(9)} component={ComponentSelection}
                            name="powerSupplyId" components={powerSupplySelection} pcDiscount={props.pcDiscount}/>
                                
                            <Field open={openId === 10} onClick={() => toggleOpen(10)} component={ComponentSelection}
                            name="operatingSystemId" components={osSelection} pcDiscount={props.pcDiscount}/>
                        </div>
                    </div>
                </div>
            </Form>)}
        </Formik>
    );
}

export default PcConfiguratorView;