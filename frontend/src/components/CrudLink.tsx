import { Link } from "react-router-dom";

const CrudLink = (props: {to: string, name: string}) => {
    return (
        <div className="col-6 col-md-4 col-lg-3 mb-2 mt-2 text-center">
            <Link to={props.to} className="btn btn-outline-secondary w-100">{props.name}</Link>
        </div>
    );
}

export default CrudLink;