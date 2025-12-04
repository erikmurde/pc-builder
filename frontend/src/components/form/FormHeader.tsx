import { Link, useNavigate } from "react-router-dom";

interface IHeaderParams {
    title: string,
    nav?: string,
    btn?: string,
    center?: boolean
}

const FormHeader = (props: IHeaderParams) => {
    if (props.btn && props.nav) {
        return (
            <div className="row content-head p-2">
                <div className={"col-9 " + (props.center ? " text-center" : "")}>
                    <h2>{props.title}</h2>
                </div>
                <div className="col-3 text-end">
                    <Link to={props.nav} className="text-decoration-none text-white">
                        <i className={"fa-solid text-white " + (props.btn.toLocaleLowerCase() === "back" ? "fa-angle-left" : "fa-file-circle-plus")}></i>
                        {props.btn}
                    </Link>
                </div>
            </div>
        );
    }

    return (
        <div className="row content-head">
            <div className={"col-12 " + (props.center ? " text-center" : "")}>
                <h2>{props.title}</h2>
            </div>
        </div>
    );
}

export default FormHeader;