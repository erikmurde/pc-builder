import { IPcComponentDTO } from "../../dto/pcComponent/IPcComponentDTO";

interface IProps {
    title: string,
    category: string,
    components: IPcComponentDTO[]
}

const CartSummaryItem = (props: IProps) => {
    let name = props
        .components.filter(c => c.categoryName === props.category)[0].componentName;

    return (
        <div className="row flex-center mt-2">
            <div className="col-lg-2 col-12 p-0">
                <h6 className="mb-0">{props.title}</h6>
            </div>
            <div className="col-lg-10 col-12 p-0">
                {name}
            </div>
        </div>
    )
}

export default CartSummaryItem;