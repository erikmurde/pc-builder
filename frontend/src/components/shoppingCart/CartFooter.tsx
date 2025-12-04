import { Link } from "react-router-dom";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";

interface IProps {
    data: ICartPcDTO[]
}

const CartFooter = (props: IProps) => {
    let totalCost = 0;

    props.data.forEach(cartItem => {
        totalCost += cartItem.pcBuild.pcComponents
            .reduce((sum, c) => sum + Number(c.price 
                * (1 - c.discountPercentage / 100)), 0) 
                * (1 - cartItem.pcBuild.discountPercentage / 100) 
                * cartItem.qty;
    });

    return (
        <div className="row mt-3 cart-footer justify-content-end">
            <div className="col-4 mt-auto mb-auto text-end">
                <h5 className="mb-0">Total - ${Math.round(totalCost * 100) / 100}</h5>
            </div>
            <div className="col-2 p-0">
                <Link 
                    role="button" 
                    className="btn btn-primary card-button" 
                    to="../checkout">
                    Checkout
                </Link>
            </div>
        </div>
    )
}

export default CartFooter;