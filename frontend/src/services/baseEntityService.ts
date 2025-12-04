import { BaseService } from "./baseService";
import { IJWTData } from "../dto/IJWTData";

export abstract class BaseEntityService<TEntityDTO, TEntityDetailsDTO, TEntityCreateDTO, TEntityEditDTO> extends BaseService {

    protected baseUrl: string;

    constructor(baseUrl: string) {
        super();
        this.baseUrl = baseUrl;
    }
    
    async getAll(jwtData?: IJWTData): Promise<TEntityDTO[] | undefined> {
        try {
            const response = await this.axios.get<TEntityDTO[]>(`${this.baseUrl}`,
            {
                params: { jwt_data: jwtData ?? null}
            });

            console.log('getAll response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }

    async getEntity(id: string, jwtData?: IJWTData): Promise<TEntityDetailsDTO | undefined> {
        try {
            const response = await this.axios.get<TEntityDetailsDTO>(`${this.baseUrl}/${id}`,
            {
                params: { jwt_data: jwtData ?? null }
            });

            console.log('getEntity response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }

    async create(entity: TEntityCreateDTO, jwtData?: IJWTData): Promise<{success: boolean, id?: string} | undefined> {
        try {
            const response = await this.axios.post(`${this.baseUrl}`, entity,
            {
                params: { jwt_data: jwtData ?? null }
            });

            console.log('create response: ', response);
            return response.data.id 
                ? {success: response.status === 201, id: response.data.id} 
                : {success: response.status === 201};

        } catch (error: any) {
            this.logError(error);
        }
    }

    async edit(id: string, entity: TEntityEditDTO, jwtData?: IJWTData): Promise<boolean | undefined> {
        try {
            const response = await this.axios.put(`${this.baseUrl}/${id}`, entity,
            {
                params: { jwt_data: jwtData ?? null }
            });

            console.log('edit response: ', response);
            return response.status === 204;

        } catch (error: any) {
            this.logError(error);
        }
    }

    async delete(id: string, jwtData?: IJWTData): Promise<boolean | undefined> {
        try {
            const response = await this.axios.delete(`${this.baseUrl}/${id}`,
            {
                params: { jwt_data: jwtData ?? null }
            });

            console.log('delete response: ', response);
            return response.status === 204;
            
        } catch (error: any) {
            this.logError(error);
        }
    }
}