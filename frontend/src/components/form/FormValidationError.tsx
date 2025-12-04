const FormValidationError = (props: {
        name: string | undefined, 
        touched: boolean | undefined
    }) => {

    if (!props.name || !props.touched) {
        return (null);
    }

    return (
        <div className="text-danger mb-3">
            {props.name}
        </div>
    );
}

export default FormValidationError;