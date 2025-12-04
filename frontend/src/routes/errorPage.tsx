import { useRouteError } from "react-router-dom";

interface IError {
    statusText?: string,
    status?: number
}

const ErrorPage = () => {
    const error = useRouteError() as IError;
    console.error(error);

    return (
        <div className="alert alert-danger text-center m-auto">
            <h1>Oops!</h1>
            <p>Sorry, an unexpected error has occurred :(</p>
            <p>
                <i>{error.status} {error.statusText}</i>
            </p>
        </div>
    );
};

export default ErrorPage;