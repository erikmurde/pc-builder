import { IComponentAttributeDTO } from "../componentAttribute/IComponentAttributeDTO";

export interface IComponentDetailsDTO {
    id: string,
    categoryId: string,
    discountId: string,
    manufacturerId: string,
    categoryName: string,
    discountName: string,
    discountPercentage: number,
    manufacturerName: string,
    componentName: string,
    description: string,
    price: string,
    stock: string,
    imageSrc?: string,
    componentAttributes: IComponentAttributeDTO[]
}