import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IPcBuildDTO } from "../../../dto/pcBuild/IPcBuildDTO";
import DeleteModal from "../../modal/DeleteModal";
import EntityImage from "../../image/EntityImage";

const PcBuildListItem = (props: IBaseItemProps<IPcBuildDTO>) => {
    return (
        <div className={"row image-entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <EntityImage src={props.entity.imageSrc} alt="Image of a PC build"/>
            <div className="col-3 col-lg-2">
                <strong>{props.entity.pcName}</strong>
            </div>
            <div className="col d-none d-lg-block"></div>
            <div className="col-2">
                {props.entity.categoryName}
            </div>
            <div className="col-2">
                {props.entity.discountPercentage + "%"}
            </div>
            <div className="col-2">
                {props.entity.stock}
            </div>
            <div className="col-3 col-lg-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
                <DeleteModal id={props.entity.id!} name="PC build" nav="" onDelete={props.onDelete!}/>
            </div>
        </div>
    );
}

export default PcBuildListItem;