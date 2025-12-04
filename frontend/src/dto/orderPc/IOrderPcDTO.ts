import { IPcBuildDTO } from "../pcBuild/IPcBuildDTO"

export interface IOrderPcDTO {
    pcBuild: IPcBuildDTO,
    packageSize: string,
    pricePerUnit: number,
    qty: number
}