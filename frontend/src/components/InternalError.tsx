interface IProps {
    message: string
}

const InternalError = (props: IProps) => {
    return (
        <div className="alert alert-danger error-window shadow text-center">
                <h3>Oops!</h3>
                <p>Sorry, an unexpected error has occurred :(</p>
                <p>{props.message}</p>
        </div>
    );
}

export default InternalError;