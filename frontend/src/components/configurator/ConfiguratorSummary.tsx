import PlaceholderImage from "../../images/placeholder.jpg";
import { IComponentDTO } from "../../dto/component/IComponentDTO";
import { FormikProps } from "formik";
import ConfiguratorErrorMessage from "./ConfiguratorErrorMessage";

interface IProps {
    components: IComponentDTO[],
    form: FormikProps<any>,
    pcDiscount: number
}

const ConfiguratorSummary = (props: IProps) => {
    let values = props.form.values;
    let pcCase = props.components.filter(c => c.id === values.caseId)[0];

    let selectedComponents = props.components.filter(c => 
        [
            values.caseId, values.motherboardId, values.processorId, 
            values.cpuCoolerId, values.memoryId, values.graphicsCardId, 
            values.primaryStorageId, values.secondaryStorageId,
            values.powerSupplyId, values.operatingSystemId
        ].includes(c.id));

    let subtotal = selectedComponents
        .reduce((sum, c) => sum + Number(c.price) 
        * (1 - c.discountPercentage / 100), 0) 
        * (1 - props.pcDiscount / 100);

    return (
        <div className="card shadow text-center" id="summary-card">
            <div id="configurator-image-container" className="flex-center">
                <img id="configurator-image" src={pcCase?.imageSrc ?? PlaceholderImage} alt="Image of a gaming PC case"/>
            </div>
            <div className="card-body d-flex flex-column align-items-center p-0">
                <ConfiguratorErrorMessage error={props.form.errors.caseId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.motherboardId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.processorId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.cpuCoolerId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.memoryId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.graphicsCardId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.primaryStorageId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.secondaryStorageId as string}/>
                <ConfiguratorErrorMessage error={props.form.errors.powerSupplyId as string}/>
                <div className="row" id="configurator-summary">
                    <div className="col-5 flex-center" id="subtotal-text">
                        Subtotal
                    </div>
                    <div className="col-7"></div>
                    <div className="col-5 flex-center" id="subtotal">
                        <h5 className="m-0">${Math.round(subtotal * 100) / 100}</h5>
                    </div>
                    <div className="col"></div>
                    <div className="col-5 card-info p-0">
                        <button type="submit" className="btn btn-primary card-button mt-0">
                            <h5 className="m-0">Add To Cart</h5>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ConfiguratorSummary;