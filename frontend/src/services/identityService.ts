import { IJwtObject } from "../domain/IJwtObject";
import { IErrorResponse } from "../dto/IErrorResponse";
import { IJWTData } from "../dto/IJWTData";
import { ILoginData } from "../dto/ILoginData";
import { ILogoutResponse } from "../dto/ILogoutResponse";
import { IRegisterData } from "../dto/IRegisterData";
import { BaseService } from "./baseService";
import jwt_decode, { JwtPayload } from "jwt-decode";

export class IdentityService extends BaseService {
    protected baseUrl = '/identity/account';

    constructor() {
        super();
    }

    async register(data: IRegisterData): Promise<IJWTData | IErrorResponse> {
        return (await this.execute(`${this.baseUrl}/register`, data));
    }

    async login(data: ILoginData): Promise<IJWTData | IErrorResponse> {
        return (await this.execute(`${this.baseUrl}/login`, data));
    }

    async refreshToken(data: IJWTData): Promise<IJWTData | IErrorResponse> {
        return (await this.execute(`${this.baseUrl}/refreshtoken`, data));
    }

    async logout(jwtData: IJWTData): Promise<ILogoutResponse | undefined> {
        try {
            const response = await this.axios.post<ILogoutResponse>(`${this.baseUrl}/logout`, 
            { refreshToken: jwtData.refreshToken },
            {
                params: { jwt_data: jwtData ?? null }
            });

            //console.log('logout response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }

    private async execute(action: string, data?: IRegisterData | ILoginData | IJWTData): Promise<IJWTData | IErrorResponse> {
        try {
            const response = await this.axios.post<IJWTData>(action, data);

            //console.log(action, ' response: ', response);
            return response.data;
            
        } catch (error: any) {
            this.logError(error);

            return {
                status: error.status, 
                errorMessage: error.errorMessage
            } as IErrorResponse
        }
    }

    jwtIsExpired(jwt: string): boolean {
        let jwtExp = (jwt_decode(jwt) as JwtPayload).exp! * 1000;
            
        if (jwtExp < new Date().getTime()) {
            return true;
        }
        return false;
    }

    getJwtObject(jwtData: IJWTData): IJwtObject {
        let jwtObject: any = jwt_decode(jwtData.jwt);

        return {
            role: jwtObject['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            email: jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
            name: jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
            nameIdentifier: jwtObject['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']
        }
    }

    isAdmin(jwtData: IJWTData | null): boolean {
        if (!jwtData) return false;
        return this.getJwtObject(jwtData).role === "Admin";
    }

    isJwtData(value: IJWTData | IErrorResponse): value is IJWTData {
        return "jwt" in value && "refreshToken" in value;
    }
}