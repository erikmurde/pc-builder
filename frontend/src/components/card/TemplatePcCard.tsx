import { IPcBuildDTO } from "../../dto/pcBuild/IPcBuildDTO";
import PlaceholderImage from "../../images/placeholder.jpg";
import { Link } from "react-router-dom";
import { useContext } from "react";
import { JwtContext } from "../../routes/root";

interface IProps {
    entity: IPcBuildDTO
}

const TemplatePcCard = (props: IProps) => {
    const { jwtData } = useContext(JwtContext);

    return (
        <div className={"col-12 col-md-6 col-xl-4 col-xxl-3 mb-3 flex-center"}>
            <div className="card pc-card shadow text-center">
                <div className="card-image-container flex-center">
                    <img className="card-img-top" src={props.entity.imageSrc ?? PlaceholderImage} alt="Image of a gaming PC"/>
                </div>
                <div className="card-body d-flex flex-column align-items-center p-0">
                    <h5 className="card-title mt-2">{props.entity.pcName}</h5>
                    <p className="card-text m-auto">{props.entity.description}</p>
                    <h5 className="mt-auto mb-1">
                        Starting at <strong className="text-danger">${Math.round(props.entity.cost)}</strong>
                    </h5>
                </div>
                <div className="card-footer p-0 border-0">
                    <Link to={jwtData ? "../configurator/" + props.entity.id : "../login"} className="btn btn-primary card-button">
                        <h5 className="mb-0">Configure</h5>
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default TemplatePcCard;