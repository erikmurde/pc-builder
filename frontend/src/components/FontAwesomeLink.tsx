import { useContext } from "react";
import { NavLink } from "react-router-dom";
import { CartCountContext } from "../routes/root";

const FontAwesomeLink = (props: {to: string, icon: string, isCart?: boolean}) => {
    const { cartCount } = useContext(CartCountContext);

    if (props.isCart && cartCount > 0) {
        return (
            <li className="nav-item">
                <NavLink to={props.to}>
                    <i className={"nav-link " + props.icon}></i>
                    <span className="cart-badge flex-center">
                        {cartCount}
                    </span>
                </NavLink>
            </li>
        );
    }

    return (
        <li className="nav-item">
            <NavLink to={props.to}><i className={"nav-link " + props.icon}></i></NavLink>
        </li>
    );
}

export default FontAwesomeLink;