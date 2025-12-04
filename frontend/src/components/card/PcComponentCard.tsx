import { IPcComponentCartDTO } from "../../dto/pcComponent/IPcComponentCartDTO";
import EntityImage from "../image/EntityImage";

interface IProps {
    entity: IPcComponentCartDTO,
    pcDiscount: number
}

const PcComponentCard = (props: IProps) => {
    let price = props.entity.price * (1 - props.entity.discountPercentage / 100) * (1 - props.pcDiscount / 100);

    return (
        <div className="card cart-component-card">
            <div className="row flex-center">
                <EntityImage src={props.entity.imageSrc} alt={"Image of a " + props.entity.categoryName}/>
                <div className="col-10">
                    <div className="card-body">
                        <div className="row">
                            <strong>{props.entity.categoryName}</strong>
                        </div>
                        <div className="row">
                            <div className="col">
                                {props.entity.componentName}<span className="d-none d-lg-inline"> - ${Math.round(price * 100) / 100}</span>
                            </div>
                        </div>
                        <div className="row d-lg-none">
                            <div className="col">
                                ${Math.round(price * 100) / 100}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PcComponentCard;