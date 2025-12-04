import { useContext } from "react";
import { JwtContext } from "../../root";
import { IdentityService } from "../../../services/identityService";

const General = () => {
    const service = new IdentityService();
    const { jwtData } = useContext(JwtContext);

    let jwtObject = service.getJwtObject(jwtData!);

    return (
        <>
            <div className="row flex-center table-head m-0 p-2 mb-3">
                <div className="col-12">
                    <h2>User Information</h2>
                </div>
            </div>
            <div className="row p-2 m-0"> 
                Username - {jwtObject.name} 
            </div>
            <div className="row p-2 m-0"> 
                Email - {jwtObject.email} 
            </div>
            <div className="row p-2 m-0"> 
                Role - {jwtObject.role ?? "Standard"} 
            </div>
        </>
    );
}

export default General;