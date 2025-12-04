import { Link } from "react-router-dom";
import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IComponentAttributeDTO } from "../../../dto/componentAttribute/IComponentAttributeDTO";
import DeleteModal from "../../modal/DeleteModal";

interface IProps extends IBaseItemProps<IComponentAttributeDTO> {
    noLinks?: boolean
}

const ComponentAttributeListItem = (props: IProps) => {
    let links = props.noLinks ? <></> :
        <div className="col-2">
            <Link 
                to={"../componentAttributes/edit/" + props.entity.id} 
                className="fa-solid fa-pen-to-square text-success">
            </Link>
            <DeleteModal id={props.entity.id} name="attribute" nav="" onDelete={props.onDelete!}/>
        </div>

    return (
        <div className={"row entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-4">
                {props.entity.attributeName}
            </div>
            <div className="col-6">
                {props.entity.attributeValue}
            </div>
            {links}
        </div>
    );
}

export default ComponentAttributeListItem