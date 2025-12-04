import { IJWTData } from "../dto/IJWTData";
import { IUserReviewCreateDTO } from "../dto/userReview/IUserReviewCreateDTO";
import { IUserReviewDTO } from "../dto/userReview/IUserReviewDTO";
import { IUserReviewEditDTO } from "../dto/userReview/IUserReviewEditDTO";
import { BaseEntityService } from "./baseEntityService";

export class UserReviewService extends BaseEntityService<IUserReviewDTO, IUserReviewDTO, IUserReviewCreateDTO, IUserReviewEditDTO> {
    constructor() {
        super('userReviews');   
    }

    async getAllUser(jwtData: IJWTData): Promise<IUserReviewDTO[] | undefined> {
        try {
            const response = await this.axios.get<IUserReviewDTO[]>(`${this.baseUrl}/user`,
            {
                params: { jwt_data: jwtData}
            });

            //console.log('getAllUser response: ', response);
            return response.data;

        } catch (error: any) {
            this.logError(error);
        }
    }
}