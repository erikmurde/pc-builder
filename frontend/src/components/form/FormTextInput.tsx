import { FieldInputProps, FormikProps } from "formik";
import FormValidationError from "./FormValidationError";

interface IProps {
    field: FieldInputProps<any>,
    form: FormikProps<any>,
    label: string,
    type?: string,
    length?: number,
    lengthLg?: number
}

const FormTextInput = (props: IProps) => {
    let field = props.field;
    let error = props.form.errors[field.name]?.toString();
    let touched = props.form.touched[field.name];

    let inputId = `input-${field.name}`;
    let isInvalid = error && touched;

    return(
        <div className={"col-" + (props.length ?? 6) + " col-lg-" + (props.lengthLg ?? 4)}>
            <div className={"form-floating " + (isInvalid ? "" : "mb-3")}>
                <input 
                className={"form-control mt-2 " + (isInvalid ? "is-invalid" : "")}
                id={inputId} 
                type={props.type ?? "text"}
                placeholder="#" 
                {...props.field}/>
                <label className="text-muted" htmlFor={inputId}>{props.label}</label>
            </div>
            <FormValidationError name={error} touched={touched !== undefined}/>
        </div>
    )
}

export default FormTextInput;