import { IPcComponentDTO } from "../../dto/pcComponent/IPcComponentDTO";

interface IProps {
    entity: IPcComponentDTO,
    index: number
}

const StorePcComponentItem = (props: IProps) => {
    return (
        <div className={"row entity-row m-0 p-2 " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <div className="col-4">
                <strong>{props.entity.categoryName}</strong>
            </div>
            <div className="col-8">
                {props.entity.componentName}
            </div>
        </div>
    );
}

export default StorePcComponentItem;