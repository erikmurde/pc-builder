import { IBaseItemProps } from "../../../domain/IBaseItemProps";
import { IOrderPcDTO } from "../../../dto/orderPc/IOrderPcDTO";
import EntityImage from "../../image/EntityImage";

const OrderPcListItem = (props: IBaseItemProps<IOrderPcDTO>) => {
    return (
        <div className={"row image-entity-row " + (props.index % 2 === 1 ? "row-odd" : "row-even")}>
            <EntityImage src={props.entity.pcBuild.imageSrc} length={2} alt="Image of a PC build"/>
            <div className="col-4">
                <strong>{props.entity.pcBuild.pcName}</strong>
            </div>
            <div className="col-3 col-lg-2">
                {props.entity.packageSize}
            </div>
            <div className="col-3 col-lg-2">
                {"€" + props.entity.pricePerUnit}
            </div>
            <div className="col-2">
                {props.entity.qty}
            </div>
        </div>
    );
}

export default OrderPcListItem;