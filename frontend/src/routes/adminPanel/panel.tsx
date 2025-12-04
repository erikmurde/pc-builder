import { useContext } from "react";
import { JwtContext } from "../root";
import { IdentityService } from "../../services/identityService";
import { Outlet } from "react-router-dom";
import NoAccess from "../../components/NoAccess";
import CrudLink from "../../components/CrudLink";

const Panel = () => {
    const service = new IdentityService();
    const { jwtData } = useContext(JwtContext);
    const isAdmin = service.isAdmin(jwtData);

    if (isAdmin) {
        return (
            <div className="row flex-center">
                <div className="col-12 col-md-10">
                    <div className="row shadow content-panel">
                        <div className="col-12 flex-center content-head" >
                            <h2>ADMIN PANEL</h2>
                        </div>
                        <CrudLink to="orders" name="Orders"/>
                        <CrudLink to="payments" name="Payments"/>
                        <CrudLink to="pcBuilds" name="PC Builds"/>
                        <CrudLink to="components" name="Components"/>
                        <CrudLink to="shippingCosts" name="Shipping Costs"/>
                        <CrudLink to="categories" name="Categories"/>
                        <CrudLink to="discounts" name="Discounts"/>
                        <CrudLink to="attributes" name="Attributes"/>
                        <CrudLink to="statuses" name="Statuses"/>
                        <CrudLink to="manufacturers" name="Manufacturers"/>
                        <CrudLink to="shippingMethods" name="Shipping Methods"/>
                        <CrudLink to="packageSizes" name="Package Sizes"/>
                    </div>
                    <div className="row">
                        <Outlet />
                    </div>
                </div>
            </div>
        );
    } else {
        return (
            <NoAccess/>
        );
    }
}

export default Panel;