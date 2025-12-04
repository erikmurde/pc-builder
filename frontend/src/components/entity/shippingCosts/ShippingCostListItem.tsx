import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IShippingCostDTO } from "../../../dto/shippingCost/IShippingCostDTO";
import DeleteModal from "../../modal/DeleteModal";

const ShippingCostListItem = (props: IBaseItemProps<IShippingCostDTO>) => {
    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-3">
                {props.entity.packageSize}
            </div>
            <div className="col-4">
                {props.entity.shippingMethod}
            </div>
            <div className="col-2 col-lg-3">
                {"$" + Math.round(parseFloat(props.entity.costPerUnit) * 100) / 100}
            </div>
            <div className="col-3 col-lg-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
                <DeleteModal id={props.entity.id!} name="shipping cost" nav="" onDelete={props.onDelete!}/>
            </div>
        </div>
    );
}

export default ShippingCostListItem;