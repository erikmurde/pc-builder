import { useContext, useMemo } from "react";
import { JwtContext } from "../routes/root";
import { IdentityService } from "../services/identityService";
import baseAxios from "../services/baseAxios";
import { IJWTData } from "../dto/IJWTData";

const Interceptor = ({children}: any) => {
    const service = new IdentityService();
    const { setJwtData } = useContext(JwtContext);
 
    useMemo(() => {        
        baseAxios.interceptors.request.use(async (config) => {
            let jwtData: IJWTData = config.params.jwt_data;

            if (jwtData) {
                if (service.jwtIsExpired(jwtData.jwt)) {
                    await service.refreshToken(jwtData).then(
                        token => {
                            if (service.isJwtData(token)) {
                                if (setJwtData) setJwtData(token);
                                config.headers.Authorization = 'Bearer ' + token.jwt;
                            }
                        }  
                    );
                } else {
                    config.headers.Authorization = 'Bearer ' + jwtData.jwt;
                }
            }
            return config;
        })
    }, []);

    return (
        <>{children}</>
    );
}

export default Interceptor;