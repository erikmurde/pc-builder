import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IStatusDTO } from "../../../dto/status/IStatusDTO";
import DeleteModal from "../../modal/DeleteModal";

const StatusListItem = (props: IBaseItemProps<IStatusDTO>) => {
    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? " row-odd" : " row-even")}>
            <div className="col-9 col-lg-10">
                {props.entity.statusName}
            </div>
            <div className="col-3 col-lg-2">
                <Link to={props.entity.id!} className="fa-solid fa-circle-info text-primary"></Link>
                <Link to={"edit/" + props.entity.id} className="fa-solid fa-pen-to-square text-success"></Link>
                <DeleteModal id={props.entity.id!} name="status" nav="" onDelete={props.onDelete!}/>
            </div>
        </div>
    );
}

export default StatusListItem;