import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IComponentDTO } from "../../../dto/component/IComponentDTO";
import DeleteModal from "../../modal/DeleteModal";
import EntityImage from "../../image/EntityImage";

const ComponentListItem = (props: IBaseItemProps<IComponentDTO>) => { 
    let price = Number(props.entity.price) * (1 - props.entity.discountPercentage / 100);

    return (
        <div className={"row image-entity-row " + (props.index % 2 === 0 ? "row-odd" : "row-even")}>
            <EntityImage src={props.entity.imageSrc} alt="Image of a PC component"/>
            <div className="col-4 col-lg-3">
                <strong>{props.entity.componentName}</strong>
            </div>
            <div className="col d-none d-lg-block"></div>
            <div className="col-3 col-lg-2">
                {props.entity.categoryName}
            </div>
            <div className="col-1 col-xl-2 d-none d-lg-block">
                {props.entity.discountPercentage + "%"}
            </div>
            <div className="col-2 col-xl-1">
                {"$" + Math.round(price * 100) / 100}
            </div>
            <div className="col-3 col-lg-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
                <DeleteModal id={props.entity.id!} name="component" nav="" onDelete={props.onDelete!}/>
            </div>
        </div>
    );
}

export default ComponentListItem;