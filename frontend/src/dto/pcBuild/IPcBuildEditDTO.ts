import { IPcBuildComponentsDTO } from "./IPcBuildComponentsDTO";

export interface IPcBuildEditDTO extends IPcBuildComponentsDTO {
    id: string,
    categoryId: string,
    discountId: string,
    pcName: string,
    description: string,
    stock: string,
    imageSrc?: string
}