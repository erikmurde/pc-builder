import { IPcComponentCartDTO } from "../pcComponent/IPcComponentCartDTO"

export interface IPcBuildCartDTO {
    id: string,
    discountPercentage: number,
    pcName: string,
    imageSrc?: string,
    isCustom: boolean,
    stock: number,
    pcComponents: IPcComponentCartDTO[]
}