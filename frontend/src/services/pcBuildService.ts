import { IPcBuildCreateDTO } from "../dto/pcBuild/IPcBuildCreateDTO";
import { IPcBuildDTO } from "../dto/pcBuild/IPcBuildDTO";
import { IPcBuildDetailsDTO } from "../dto/pcBuild/IPcBuildDetailsDTO";
import { IPcBuildEditDTO } from "../dto/pcBuild/IPcBuildEditDTO";
import { IPcBuildStoreDTO } from "../dto/pcBuild/IPcBuildStoreDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";

export class PcBuildService extends BaseEntityServiceEdit<IPcBuildDTO, IPcBuildDetailsDTO, IPcBuildCreateDTO, IPcBuildEditDTO> {
    constructor() {
        super('pcBuilds');   
    }

    async getAllStore(): Promise<IPcBuildStoreDTO[] | undefined> {
        try {
            const response = await this.axios.get<IPcBuildStoreDTO[]>(`${this.baseUrl}/store`,
            {
                params: { jwt_data: null}
            });

            //console.log('getAllStore response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }
}