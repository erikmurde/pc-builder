import PlaceholderImage from "../../images/placeholder.jpg";
import { Link } from "react-router-dom";
import { IPcBuildStoreDTO } from "../../dto/pcBuild/IPcBuildStoreDTO";

interface IProps {
    entity: IPcBuildStoreDTO,
    minXxl?: number,
    minXl?: number,
    nav? :string

    clearFilters?: () => void
}

const PrebuiltPcCard = (props: IProps) => {
    let cpu = props.entity.pcComponents
        .filter(c => c.categoryName === "Processor")[0].componentName;
    let gpu = props.entity.pcComponents
        .filter(c => c.categoryName === "Graphics Card")[0].componentName;
    let memory = props.entity.pcComponents
        .filter(c => c.categoryName === "Memory")[0].componentName;
    let motherboard = props.entity.pcComponents
        .filter(c => c.categoryName === "Motherboard")[0].componentName;

    let costNoDiscount = props.entity.pcComponents
        .reduce((sum, c) => sum + c.price * (1 - c.discountPercentage / 100), 0);

    let costDiscount = costNoDiscount * (1 - props.entity.discountPercentage / 100);

    let hasDiscount = costDiscount < costNoDiscount;

    let oldPriceText = hasDiscount ?
        <div className="row mt-auto">
            <div className="col text-start">
                <s className="text-muted">${Math.round(costNoDiscount)}</s>
            </div>
        </div>  
        : <></>

    let discountText = hasDiscount ?
        <div className="col-4 bg-danger text-white rounded flex-center">
            ${Math.round(costNoDiscount - costDiscount)} OFF
        </div>
        : <></>

    let fullStars = [];
    let emptyStars = [];
    for (let index = 0; index < 5; index++) {
        index < props.entity.reviewScore
        ? fullStars.push(<i key={index} className="fas fa-star text-warning"></i>)
        : emptyStars.push(<i key={index} className="far fa-star text-warning"></i>);
    }

    return (
        <div className={"col-12 col-md-6 col-xl-" + (props.minXl ?? "4") + " col-xxl-" + (props.minXxl ?? "3") + " mb-3 flex-center"}>
            <div className="card pc-card prebuilt-pc-card shadow">
                <div className="card-image-container flex-center">
                    <img className="card-img-top" src={props.entity.imageSrc ?? PlaceholderImage} alt="Image of a gaming PC"/>
                </div>
                <div className="card-body d-flex flex-column">
                    <h5 className="card-title">{props.entity.pcName}</h5>
                    <div className="row mb-2">
                        <div className="col">
                            {fullStars}
                            {emptyStars}
                            &nbsp;({props.entity.numOfReviews})
                        </div>
                    </div>
                    <div className="row mb-2">
                        <p className="mb-1">{cpu}</p>
                        <p className="mb-1">{gpu}</p>
                        <p className="mb-1">{memory}</p>
                        <p className="mb-0">{motherboard}</p>
                    </div>
                    {oldPriceText}
                    <div className={"row " + (hasDiscount ? "" : "mt-auto")}>
                        <div className="col-7">
                            <h3 className="mb-0">
                                <strong>${Math.round(costDiscount)}</strong>
                            </h3>
                        </div>
                        {discountText}
                    </div>
                </div>
                <div className="card-footer p-0 border-0">
                    <Link to={props.nav ?? props.entity.id} className="btn btn-primary card-button" onClick={props.clearFilters}>
                        <h5 className="mb-0">See Details</h5>
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default PrebuiltPcCard;