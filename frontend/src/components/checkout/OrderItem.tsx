import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";

interface IProps {
    entity: ICartPcDTO,
    itemCost: number
}

const OrderItem = (props: IProps) => {
    let pcBuild = props.entity.pcBuild;

    return (
        <div className="row text-start ps-2 pe-2 p-1">
            <div className="col-9">
                {props.entity.qty} x {pcBuild.pcName}
            </div>
            <div className="col-3 text-end">
                ${Math.round(props.itemCost * 100) / 100}
            </div>
        </div>
    );
}

export default OrderItem;