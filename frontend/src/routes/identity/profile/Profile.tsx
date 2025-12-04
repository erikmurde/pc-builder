import { Link, Outlet } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { useContext } from "react";
import { JwtContext } from "../../root";
import NoAccess from "../../../components/NoAccess";

const Profile = () => {
    const { jwtData } = useContext(JwtContext);

    if (!jwtData) {
        return (
            <NoAccess />
        );
    }

    return (
        <div className="row justify-content-center">
            <div className="col-12 col-xl-10">
                <div className="row content-panel">
                    <div className="col-12 col-xl-2 border-right">
                        <FormHeader title="Profile" center/>
                        <div className="row">
                            <div className="col-4 col-xl-12 mt-2 mb-2">
                                <Link to="general" className="btn btn-outline-primary card-button">General</Link>
                            </div>
                            <div className="col-4 col-xl-12 mt-2 mb-2">
                                <Link to="orders" className="btn btn-outline-primary card-button">My Orders</Link>
                            </div>
                            <div className="col-4 col-xl-12 mt-2 mb-2">
                                <Link to="payments" className="btn btn-outline-primary card-button">My Payments</Link>
                            </div>
                            <div className="col-4 col-xl-12 mt-2 mb-2">
                                <Link to="reviews" className="btn btn-outline-primary card-button">My Reviews</Link>
                            </div>
                        </div>
                    </div>
                    <div className="col-12 col-xl-10">
                        <Outlet />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Profile;