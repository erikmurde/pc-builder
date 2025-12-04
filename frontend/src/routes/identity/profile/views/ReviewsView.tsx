import NoItemsMessage from "../../../../components/profile/NoItemsMessage";

interface IProps {
    reviews: JSX.Element[]
}

const ReviewsView = (props: IProps) => {
    return (
        <> 
            <div className="row flex-center table-head m-0 mb-3 p-2">
                <div className="col-12">
                    <h2>My Reviews</h2>
                </div>
            </div>
            <NoItemsMessage list={props.reviews} message="You have not written any reviews."/>
            {props.reviews}
        </>
    );
}

export default ReviewsView;