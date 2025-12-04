import { NavLink } from "react-router-dom";

const HeaderLink = (props: {to: string, name: string, onClick?: () => void}) => {
    return (
        <li className="nav-item">
            <NavLink 
                to={props.to} 
                className={({ isActive }) =>
                    isActive ? "nav-link text-primary" : "nav-link text-dark"
                }
                onClick={props.onClick}>
                {props.name}
            </NavLink>
        </li>
    );
}

export default HeaderLink;