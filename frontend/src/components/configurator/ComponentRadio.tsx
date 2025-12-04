import { Field, FieldInputProps, FormikProps } from "formik";
import { IComponentDTO } from "../../dto/component/IComponentDTO";
import EntityImage from "../image/EntityImage";
import ComponentInfoModal from "../modal/ComponentInfoModal";

interface IProps {
    field: FieldInputProps<any>,
    form: FormikProps<any>,
    index: string,
    entity: IComponentDTO,
    pcDiscount: number,
    selected: IComponentDTO
}

const ComponentRadio = (props: IProps) => {
    if (!props.entity || !props.selected) {
        return (
            <div>
                Loading...
            </div>
        );
    }

    let inputId = `${props.field.name}-${props.index}`;

    // Component prices that are shown include both the component discount as well as the PC wide discount
    // This is to reduce user confusion

    let price = Number(props.entity.price) * (1 - props.entity.discountPercentage / 100) * (1 - props.pcDiscount / 100);
    let selectedPrice = Number(props.selected.price) * (1 - props.selected.discountPercentage / 100) * (1 - props.pcDiscount / 100);

    let priceDiff = Math.round(price - selectedPrice);

    let isSelected = props.entity.id === props.selected.id

    let priceDiffText: string;
    if (isSelected) priceDiffText = "";
    else if (priceDiff === 0) priceDiffText = "$" + priceDiff;
    else priceDiffText = priceDiff > 0 ? "+$" + priceDiff : "-$" + priceDiff * -1;

    return (
        <label className="select-item mb-2">
            <Field type="radio" id={inputId} name={props.field.name} value={props.entity.id} className="component-radio-input"/>
            <div className="card component-select-card flex-center">
                <div className="row w-100 h-100 flex-center">
                    <EntityImage src={props.entity.imageSrc} alt={"Image of " + props.entity.categoryName} length={2}/>
                    <div className="col-8 col-lg-6 col-xl-7 text-start">
                        {props.entity.componentName}
                    </div>
                    <div className="col-4 col-lg-4 col-xl-3 h-75 d-flex flex-column justify-content-between text-end">
                        <ComponentInfoModal id={props.entity.id}/>
                        <div className="row">
                            <div className="col-12 text-primary">
                                {isSelected ? "Selected" : ""}
                            </div>
                            <div className="col-12">
                                {priceDiffText} {priceDiffText ? `($${Math.round(Number(price))})` : ""}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </label>
    );
}

export default ComponentRadio;