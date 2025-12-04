import { IPcBuildComponentsDTO } from "./IPcBuildComponentsDTO";

export interface IPcBuildCreateDTO extends IPcBuildComponentsDTO {
    categoryId: string,
    discountId: string,
    pcName: string,
    description: string,
    stock: string,
    imageSrc?: string
}