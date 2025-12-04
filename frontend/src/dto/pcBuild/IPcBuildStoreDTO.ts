import { IPcComponentStoreDTO } from "../pcComponent/IPcComponentStoreDTO";

export interface IPcBuildStoreDTO {
    id: string,
    discountPercentage: number,
    pcName: string,
    stock: string,
    imageSrc?: string,
    numOfReviews: number,
    reviewScore: number,
    pcComponents: IPcComponentStoreDTO[]
}