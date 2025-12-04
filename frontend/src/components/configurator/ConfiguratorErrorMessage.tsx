interface IProps {
    error: string
}

const ConfiguratorErrorMessage = (props: IProps) => {
    if (!props.error) {
        return (<></>);
    }

    return (
        <div className="row text-danger mt-2 mb-2">
            <div className="col">
                <strong>{props.error}</strong>
            </div>
        </div>
    );
}

export default ConfiguratorErrorMessage;