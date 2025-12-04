import { Field, FieldInputProps, FormikProps } from "formik";
import { Collapse } from "react-bootstrap";
import { IComponentDTO } from "../../dto/component/IComponentDTO";
import EntityImage from "../image/EntityImage";
import ComponentRadio from "./ComponentRadio";

interface IProps {
    field: FieldInputProps<any>,
    form: FormikProps<any>,
    components: IComponentDTO[],
    pcDiscount: number,
    open: boolean,
    onClick: () => void
}

const ComponentSelection = (props: IProps) => {
    let inputId = `input-${props.field.name}`

    let selected = props.components
        .filter(c => c.id == props.field.value)[0];

    let selection: JSX.Element[] = [];
    for (let index = 0; index < props.components.length; index++) {
        selection.push(
            <Field 
                name={props.field.name} 
                index={index} 
                entity={props.components[index]} 
                selected={selected} 
                pcDiscount={props.pcDiscount}
                key={inputId + "-" + index} 
                component={ComponentRadio}/>
        );   
    }

    return (
        <>
            <div className="row configurator-row text-start">
                <EntityImage src={selected?.imageSrc} alt={"Picture of a " + selected?.categoryName} length={2}/>
                <div className="col-9 col-lg-8 col-xl-8">
                    <strong>{selected?.categoryName ?? "loading..."}</strong>
                    <br/>
                    {selected?.componentName ?? "loading..."}
                </div>
                <div className="col col-lg-2 p-0 pe-2">
                    <button
                        type="button"
                        className="btn btn-outline-primary configurator-btn" 
                        aria-expanded={props.open} 
                        aria-controls={inputId} 
                        onClick={props.onClick}>
                        {props.open ? "Close" : "Choose"}
                    </button>
                </div>
            </div>
            <Collapse in={props.open}>
                <div id={inputId} role="group">
                    {selection}
                    <hr className="config-hr"/>
                </div>
            </Collapse>
        </>
    );
}

export default ComponentSelection;