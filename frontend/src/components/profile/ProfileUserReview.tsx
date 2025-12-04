import { Link } from "react-router-dom";
import { IUserReviewDTO } from "../../dto/userReview/IUserReviewDTO";
import DeleteModal from "../modal/DeleteModal";

interface IProps {
    entity: IUserReviewDTO,

    onDelete: (id: string) => void
}

const ProfileUserReview = (props: IProps) => {
    let fullStars = [];
    let emptyStars = [];
    for (let index = 0; index < 5; index++) {
        index < props.entity.rating
        ? fullStars.push(<i key={index} className="fas fa-star text-warning"></i>)
        : emptyStars.push(<i key={index} className="far fa-star text-warning"></i>);
    }

    return (
        <div className="row profile-order shadow-sm flex-center p-2 mb-2">
            <div className="col-10">
                Written on <strong>{new Date(props.entity.reviewDate).toDateString()}</strong> 
            </div>
            <div className="col-2 text-end">
                <DeleteModal id={props.entity.id} name="review" nav="" linkName="remove" onDelete={props.onDelete}/>
            </div>
            <div className="col-10 mb-2">
                {fullStars}
                {emptyStars}
            </div>
            <div className="col-2 text-end">
                <Link 
                    className="text-decoration-none text-success" 
                    to={"../../review/edit/" + props.entity.id}>
                    edit
                </Link>
            </div>
            <div className="col-12 text-end">
                <Link 
                    className="text-decoration-none"
                    to={"../../prebuilt-pcs/" + props.entity.pcBuildId}>
                    PC build
                </Link>
            </div>
            <div className="col-12">
                {props.entity.reviewContent}
            </div>
        </div>
    );
}

export default ProfileUserReview;