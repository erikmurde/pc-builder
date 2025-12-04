import { IPcComponentDTO } from "../pcComponent/IPcComponentDTO";
import { IUserReviewDTO } from "../userReview/IUserReviewDTO";

export interface IPcBuildDetailsDTO {
    id: string,
    categoryId: string,
    discountId: string,
    categoryName: string,
    discountName: string,
    discountPercentage: number,
    pcName: string,
    description: string,
    stock: string,
    imageSrc?: string,
    pcComponents: IPcComponentDTO[],
    userReviews: IUserReviewDTO[]
}