import { Link, NavLink, useNavigate } from "react-router-dom";
import { useContext } from "react";
import { JwtContext } from "../routes/root";
import { IJWTData } from "../dto/IJWTData";
import { IdentityService } from "../services/identityService";
import FontAwesomeLink from "./FontAwesomeLink";
import HeaderLink from "./HeaderLink";

const Header = () => {
    const { jwtData, setJwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new IdentityService();
    const isAdmin = service.isAdmin(jwtData);

    const logout = async () => {
        if (!jwtData) return;

        var response = await service.logout(jwtData);
        if (!response) return;

        console.log('deleted ', response.tokenDeleteCount, ' tokens.');
        if (setJwtData) setJwtData(null as IJWTData | null);
        return navigate('/home');
    }

    return (
        <header className="border-bottom">
            <nav className="navbar navbar-expand navbar-toggleable-sm navbar-light">
                <div className="container">
                    <NavLink to="home" className="navbar-brand">PC Builder</NavLink>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className={isAdmin ? "d-none" : "d-md-none"}></ul>
                        <ul className="navbar-nav d-none d-md-inline-flex">
                            <HeaderLink to="home" name="Home"/>
                            <HeaderLink to="prebuilt-pcs" name="Gaming Prebuilts"/>
                            <HeaderLink to="templates" name="Customize a PC"/>
                        </ul>
                        <ul className={isAdmin ? "navbar-nav flex-grow-1" : "d-none"}>
                            <HeaderLink to="panel" name="Admin Panel"/>
                        </ul>
                        <ul className={jwtData ? "navbar-nav" : "d-none"}>
                            <FontAwesomeLink to="cart" icon="fa-solid fa-cart-shopping" isCart/>
                            <FontAwesomeLink to="profile/general" icon="fa-solid fa-user"/>
                            <li className="nav-item">
                                <Link to="#" className="nav-link text-dark" onClick={() => logout()}>
                                    Logout
                                </Link>
                            </li>
                        </ul>
                        <ul className={jwtData ? "d-none" : "navbar-nav"}>
                            <HeaderLink to="register" name="Sign up"/>
                            <HeaderLink to="login" name="Login"/>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;