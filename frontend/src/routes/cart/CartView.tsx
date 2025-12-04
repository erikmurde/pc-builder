import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import FormHeader from "../../components/form/FormHeader";
import CartFooter from "../../components/shoppingCart/CartFooter";

interface IProps {
    data: ICartPcDTO[],
    cartItems: JSX.Element[]
}

const CartView = (props: IProps) => {
    return (
        <div className="row justify-content-center">
            <div className="col-md-9 content-panel text-center shadow">
                <FormHeader title="SHOPPING CART"/>
                {props.cartItems}
                <CartFooter data={props.data}/>
            </div>
        </div>
    );
}

export default CartView;