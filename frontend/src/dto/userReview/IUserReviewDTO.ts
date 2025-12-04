export interface IUserReviewDTO {
    id: string,
    pcBuildId: string,
    userEmail: string,
    rating: number,
    reviewDate: Date,
    reviewContent: string
}