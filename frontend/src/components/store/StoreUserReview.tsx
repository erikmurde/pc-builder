import { Link } from "react-router-dom";
import { IUserReviewDTO } from "../../dto/userReview/IUserReviewDTO";
import { IdentityService } from "../../services/identityService";
import { useContext } from "react";
import { JwtContext } from "../../routes/root";
import DeleteModal from "../modal/DeleteModal";
import { UserReviewService } from "../../services/userReviewService";

interface IProps {
    entity: IUserReviewDTO,

    fetchPcBuild: () => void
}

const StoreUserReview = (props: IProps) => {
    const { jwtData } = useContext(JwtContext);
    const identityService = new IdentityService();
    const userReviewService = new UserReviewService();

    const onDelete = async(id: string) => {
        if (!jwtData) return;

        let response = await userReviewService.delete(id, jwtData);
        if (response) props.fetchPcBuild();
    }

    let fullStars = [];
    let emptyStars = [];
    for (let index = 0; index < 5; index++) {
        index < props.entity.rating
        ? fullStars.push(<i key={index} className="fas fa-star text-warning"></i>)
        : emptyStars.push(<i key={index} className="far fa-star text-warning"></i>);
    }

    let email = jwtData ? identityService.getJwtObject(jwtData).email : "";
    let belongsToUser = email === props.entity.userEmail;

    let editLink = belongsToUser ?   
    <div className="col-2 text-end">
        <Link 
            className="text-decoration-none text-success" 
            to={"../review/edit/" + props.entity.id}>
            edit
        </Link>
    </div>
    : <></>;

    let removeLink = email === props.entity.userEmail ?
    <div className="col-2 text-end">
        <DeleteModal id={props.entity.id} name="review" nav="" linkName="remove" onDelete={onDelete}/>
    </div>
    : <></>

    return (
        <div className={"row profile-order shadow-sm p-2 mb-2" + (belongsToUser ? " border-warning" : "")}>
            <div className={"col-10" + (belongsToUser ? "" : " d-none")}>
                <strong>Your Review</strong>
            </div>
            {removeLink}
            <div className="col-10"> 
                <strong>{new Date(props.entity.reviewDate).toDateString()}</strong> 
            </div>
            {editLink}
            <div className="col-12 mb-2">
                {fullStars}
                {emptyStars}
            </div>
            <div className="col-12">
                {props.entity.reviewContent}
            </div>
        </div>
    );
}

export default StoreUserReview;