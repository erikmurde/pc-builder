import { IPcBuildCartDTO } from "../pcBuild/IPcBuildCartDTO";

export interface ICartPcDTO {
    id: string,
    pcBuild: IPcBuildCartDTO,
    qty: number
}