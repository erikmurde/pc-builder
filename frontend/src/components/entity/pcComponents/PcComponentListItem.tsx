import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IPcComponentDTO } from "../../../dto/pcComponent/IPcComponentDTO";
import EntityImage from "../../image/EntityImage";

const PcComponentListItem = (props: IBaseItemProps<IPcComponentDTO>) => {
    return (
        <div className={"row image-entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <EntityImage src={props.entity.imageSrc} alt="Image of a PC component"/>
            <div className="col-5">
                {props.entity.componentName}
            </div>
            <div className="col"></div>
            <div className="col-3">
                {props.entity.categoryName}
            </div>
            <div className="col-2">
                {"$" + props.entity.price}
            </div>
        </div>
    );
}

export default PcComponentListItem;