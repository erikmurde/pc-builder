import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useNavigate } from "react-router-dom";
import { UserReviewService } from "../../../services/userReviewService";
import { IUserReviewDTO } from "../../../dto/userReview/IUserReviewDTO";
import ReviewsView from "./views/ReviewsView";
import ProfileUserReview from "../../../components/profile/ProfileUserReview";

const Reviews = () => {
    const userReviewService = new UserReviewService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const [data, setData] = useState([] as IUserReviewDTO[]);

    useEffect(() => { 
        if (!jwtData) return navigate("../login");
        getAll(); 
    }, [jwtData]);

    const getAll = async () => {
        let response = await userReviewService.getAllUser(jwtData!);
        if (response) setData(response);
    }

    const onDelete = async(id: string) => {
        if (!jwtData) return;

        let response = await userReviewService.delete(id, jwtData);
        if (response) getAll();
    }

    let reviews: JSX.Element[] = [];
    data.forEach((review, index) => {
        reviews.push(<ProfileUserReview key={index} entity={review} onDelete={onDelete}/>);
    });

    return (
        <ReviewsView reviews={reviews}/>
    );
}

export default Reviews;