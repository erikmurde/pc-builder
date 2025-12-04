import { useContext, useState, useEffect } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import FormHeader from "../../components/form/FormHeader";
import { PcBuildService } from "../../services/pcBuildService";
import StorePcComponentItem from "../../components/store/StorePcComponentItem";
import { IPcBuildDetailsDTO } from "../../dto/pcBuild/IPcBuildDetailsDTO";
import ProfileHeader from "../../components/profile/ProfileHeader";
import { CartCountContext, JwtContext } from "../root";
import { CartPcService } from "../../services/cartPcService";
import StoreUserReview from "../../components/store/StoreUserReview";
import StoreItemContent from "../../components/store/StoreItemContent";

const StorePcDetails = () => {
    const { id } = useParams();
    const { jwtData } = useContext(JwtContext);
    const { cartCount, setCartCount } = useContext(CartCountContext);
    const pcBuildService = new PcBuildService();
    const cartPcService = new CartPcService();
    const navigate = useNavigate();
    const [data, setData] = useState({} as IPcBuildDetailsDTO);

    useEffect(() => {
        if (!id) return navigate("../");

        fetchPcBuild();
    }, [id]);
    
    const fetchPcBuild = async() => {
        let pcBuild = await pcBuildService.getEntity(id!);
        pcBuild?.pcComponents.sort((a, b) => a.componentName > b.componentName ? 1 : -1);
        
        if (pcBuild) setData(pcBuild);
        else return navigate("../")
    }

    const addToCart = async () => {
        if (!jwtData) return navigate("../login");
        if (!data) return;

        // Create cart PC
        let cartPc = await cartPcService.create({
            pcBuildId: data.id,
            qty: 1
        }, jwtData)

        if (cartPc) {
            if (setCartCount) setCartCount(cartCount + 1);
            navigate("../cart");
        } 
    }

    let avgRating = data.userReviews?.length > 0
        ? Math.floor(data.userReviews.reduce((sum, u) => sum + u.rating, 0) / data.userReviews.length)
        : 0;

    let fullStars = [];
    let emptyStars = [];
    for (let index = 0; index < 5; index++) {
        index < avgRating
        ? fullStars.push(<i key={index} className="fas fa-lg fa-star text-warning"></i>)
        : emptyStars.push(<i key={index} className="far fa-lg fa-star text-warning"></i>);
    }

    let ratingText = "ratings";
    if (data.userReviews && data.userReviews.length === 1) ratingText = "rating";

    return (
        <div className="row flex-center">
            <div className="col-8 content-panel pb-2">
                <FormHeader title={data.pcName} btn="Back" nav="../prebuilt-pcs"/>
                <StoreItemContent entity={data} addToCart={addToCart}/>
                <ProfileHeader name="Description"/>
                <div className="row m-0 p-2">
                    <div className="col">
                        {data.description}
                    </div>
                </div>
                <ProfileHeader name="Specification"/>
                {data.pcComponents ? data.pcComponents.map((pcComponent, index) => 
                    <StorePcComponentItem key={index} index={index} entity={pcComponent}/>
                ) : []}
                <ProfileHeader name="Customer Reviews"/>
                <div className="row align-items-center mb-3">
                    <div className="col-4 col-xl-3 text-center">
                        <p className="display-4 mb-0">{avgRating}</p>
                        {fullStars}
                        {emptyStars}
                    </div>
                    <div className="col-4 col-xl-6">
                        Based on <strong>{data.userReviews?.length ?? 0}</strong> {ratingText}
                    </div>
                    <div className="col-4 col-xl-3 text-end">
                        <Link
                            to={"../review/" + data.id} 
                            className="btn btn-primary rounded-0">
                            Write a Review
                        </Link>
                    </div>
                </div>
                {data.userReviews ? data.userReviews
                    .sort((a, b) => a.reviewDate > b.reviewDate ? -1 : 1)
                    .map((review, index) => <StoreUserReview key={index} entity={review} fetchPcBuild={fetchPcBuild}/>
                ) : []}
            </div>
        </div>
    );
}

export default StorePcDetails;