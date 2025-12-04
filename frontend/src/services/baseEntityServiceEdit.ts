import { IJWTData } from "../dto/IJWTData";
import { BaseEntityService } from "./baseEntityService";

export class BaseEntityServiceEdit<TEntityDTO, TEntityDetailsDTO, TEntityCreateDTO, TEntityEditDTO> 
    extends BaseEntityService<TEntityDTO, TEntityDetailsDTO, TEntityCreateDTO, TEntityEditDTO> {

        constructor(baseUrl: string) {
            super(baseUrl);
        }

        async getEntityEdit(id: string, jwtData?: IJWTData): Promise<TEntityEditDTO | undefined> {
            try {
                const response = await this.axios.get<TEntityEditDTO>(`${this.baseUrl}/edit/${id}`,
                {
                    params: { jwt_data: jwtData ?? null }
                });
    
                //console.log('getEntityEdit response: ', response);
                return response.data;
    
            } catch (error: any) {
                this.logError(error);
            }
        }
    }