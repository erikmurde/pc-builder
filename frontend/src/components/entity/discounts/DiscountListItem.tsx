import { Link } from "react-router-dom";
import DeleteModal from "../../modal/DeleteModal";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";

const DiscountListItem = (props: IBaseItemProps<IDiscountDTO>) => {
    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-5">
                {props.entity.discountName}
            </div>
            <div className="col-4 col-lg-5">
                {props.entity.discountPercentage + "%"}
            </div>
            <div className="col-3 col-lg-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
                <DeleteModal id={props.entity.id!} name="discount" nav="" onDelete={props.onDelete!}/>
            </div>
        </div>
    );
}

export default DiscountListItem;