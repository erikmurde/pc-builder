import { FieldInputProps, FormikProps } from "formik";
import FormValidationError from "./FormValidationError";

interface ITextAreaInputProps {
    field: FieldInputProps<any>,
    form: FormikProps<any>,
    label: string,
    length?: number
}

const FormTextAreaInput = (props: ITextAreaInputProps) => {
    let field = props.field;
    let error = props.form.errors[field.name]?.toString();
    let touched = props.form.touched[field.name];

    let inputId = `input-${field.name}`;
    let isInvalid = error && touched;

    return (
        <div className={"col-" + (props.length ?? 8)}>
            <div className={"form-floating " + (isInvalid ? "" : "mb-3")}>
                <textarea 
                className={"form-control mt-2 " + (isInvalid ? "is-invalid" : "")}
                id={inputId} 
                placeholder="#"
                {...props.field}/>
                <label htmlFor={inputId} className="text-muted">{props.label}</label>
            </div>
            <FormValidationError name={error} touched={touched !== undefined}/>
        </div>
    );
}

export default FormTextAreaInput;