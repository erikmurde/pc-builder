import { IJWTData } from "../dto/IJWTData";
import { IComponentCreateDTO } from "../dto/component/IComponentCreateDTO";
import { IComponentDTO } from "../dto/component/IComponentDTO";
import { IComponentDetailsDTO } from "../dto/component/IComponentDetailsDTO";
import { IComponentEditDTO } from "../dto/component/IComponentEditDTO";
import { IComponentSimpleDTO } from "../dto/component/IComponentSimpleDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";

export class ComponentService extends BaseEntityServiceEdit<IComponentDTO, IComponentDetailsDTO, IComponentCreateDTO, IComponentEditDTO> {
    constructor() {
        super('components');   
    }

    async getAllSimple(): Promise<IComponentSimpleDTO[] | undefined> {
        try {
            const response = await this.axios.get<IComponentSimpleDTO[]>(`${this.baseUrl}/simple`,
            {
                params: { jwt_data: null}
            });

            //console.log('getAllSimple response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }

    async getAllMotherboard(jwtData: IJWTData): Promise<IComponentDetailsDTO[] | undefined> {
        try {
            const response = await this.axios.get<IComponentDetailsDTO[]>(`${this.baseUrl}/motherboard`,
            {
                params: { jwt_data: jwtData}
            });

            //console.log('getAllMotherboard response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }

    async getAllSelected(pcBuildId: string, jwtData: IJWTData): Promise<IComponentDetailsDTO[] | undefined> {
        try {
            const response = await this.axios.get<IComponentDetailsDTO[]>(`${this.baseUrl}/selected/${pcBuildId}`,
            {
                params: { jwt_data: jwtData}
            });

            //console.log('getAllSelected response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }
}