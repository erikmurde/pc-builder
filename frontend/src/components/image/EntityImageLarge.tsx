import PlaceholderImage from "../../images/placeholder.jpg"
import Nvidia1 from "../../images/nvidia1.png";
import Nvidia2 from "../../images/nvidia2.png";
import Nvidia3 from "../../images/nvidia3.png";
import Nvidia4 from "../../images/nvidia4.png";
import Nvidia5 from "../../images/nvidia5.png";
import Nvidia6 from "../../images/nvidia6.png";
import Amd1 from "../../images/amd1.png";
import Amd2 from "../../images/amd2.png";
import Amd3 from "../../images/amd3.png";
import Amd4 from "../../images/amd4.png";
import Intel1 from "../../images/intel1.png";
import Intel2 from "../../images/intel2.png";

interface IProps {
    src?: string,
    alt: string,
    isNotRow?: boolean,
    isStore?: boolean
}

const EntityImageLarge = (props: IProps) => {
    let source = props.src ?? PlaceholderImage;

    switch (props.src) {
        case "nvidia1.png": source = Nvidia1; break;
        case "nvidia2.png": source = Nvidia2; break;
        case "nvidia3.png": source = Nvidia3; break;
        case "nvidia4.png": source = Nvidia4; break;
        case "nvidia5.png": source = Nvidia5; break;
        case "nvidia6.png": source = Nvidia6; break;
        case "amd1.png": source = Amd1; break;
        case "amd2.png": source = Amd2; break;
        case "amd3.png": source = Amd3; break;
        case "amd4.png": source = Amd4; break;
        case "intel1.png": source = Intel1; break;
        case "intel2.png": source = Intel2; break;
    }

    if (props.isNotRow) {
        return (
            <div className="col">
                <div className={props.isStore ? "image-container-store" : "image-container-large"}>
                    <img className="entity-image-large" src={source} alt={props.alt}></img>
                </div>
            </div>
        );
    }

    return (
        <div className="row image-entity-row-large row-odd">
            <div className="col-12">
                <div className={props.isStore ? "image-container-store" : "image-container-large"}>
                    <img className="entity-image-large" src={source} alt={props.alt}></img>
                </div>
            </div>
        </div>
    );
}

export default EntityImageLarge;