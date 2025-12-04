import { FieldInputProps, FormikProps } from "formik";
import FormValidationError from "./FormValidationError";

interface ISelectInputProps {
    field: FieldInputProps<any>,
    form: FormikProps<any>,
    label: string,
    selectValues: {name: string, value: string}[],
    length?: number,
    lengthLg?: number
}

const FormSelectInput = (props: ISelectInputProps) => {
    let field = props.field;
    let error = props.form.errors[field.name]?.toString();
    let touched = props.form.touched[field.name];

    let inputId = `input-${field.name}`;
    let isInvalid = error && touched;

    let selectValues: JSX.Element[] = [];
    props.selectValues.forEach(elem => {
        selectValues.push(<option key={elem.value} value={elem.value}>{elem.name}</option>);
    });

    return (
        <div className={"col-" + (props.length ?? 6) + " col-lg-" + (props.lengthLg ?? 4)}>
            <div className={"form-floating " + (isInvalid ? "" : "mb-3")}>
                <select 
                    className={"form-select mt-2 " + (isInvalid ? "is-invalid" : "")} 
                    id={inputId} 
                    placeholder="#"
                    {...props.field}>

                    <option hidden value="">Choose...</option>
                    {selectValues}
                </select>
                <label htmlFor={inputId}>{props.label}</label>
            </div>
            <FormValidationError name={error} touched={touched !== undefined}/>
        </div>
    );
}

export default FormSelectInput;