import { IPcBuildDetailsDTO } from "../../dto/pcBuild/IPcBuildDetailsDTO";
import EntityImageLarge from "../image/EntityImageLarge";

interface IProps {
    entity: IPcBuildDetailsDTO,
    
    addToCart: () => void
}

const StoreItemContent = (props: IProps) => {
    let costNoDiscount = props.entity.pcComponents ? props.entity.pcComponents
        .reduce((sum, c) => sum + c.price * (1 - c.discountPercentage / 100), 0) : 0;

    let costDiscount = costNoDiscount * (1 - props.entity.discountPercentage / 100);

    let hasDiscount = costDiscount < costNoDiscount;

    let oldPriceText = hasDiscount
        ? <s className="text-muted">${Math.round(costNoDiscount * 100) / 100}</s>
        : <></>

    let discountText = hasDiscount ? 
        <div className="row mt-2">
            <div className="col-8 col-xl-6 bg-danger text-white rounded p-1 text-center shadow-sm">
                You Save ${Math.round(costNoDiscount - costDiscount)}
            </div>
        </div>
        : <></>

    let stockText = Number(props.entity.stock) > 0 
        ? <span className="text-success"><h5>In Stock</h5></span>
        : <span className="text-danger"><h5>Out Of Stock</h5></span>;
        
    return (
        <div className="row mt-2">
            <EntityImageLarge src={props.entity.imageSrc} alt="Image of a gaming PC" isNotRow isStore/>
            <div className="col-5 col-lg-4 m-auto">
                <div className="row">
                    {stockText}
                    <hr className="mt-2"/>
                </div>
                {oldPriceText}
                <h3 className="mb-0">
                    <strong>${Math.round(costDiscount * 100) / 100}</strong>
                </h3>
                {discountText}
            </div>
            <div className="col"></div>
            <div className="col-3 col-sm-4 col-lg-3 col-xxl-2 h-25 mt-auto ms-auto">
                <button 
                    className={"btn btn-primary card-button shadow-sm" + (Number(props.entity.stock) > 0 ? "" : " disabled")}
                    onClick={props.addToCart}>
                    Add To Cart
                </button>
            </div>
        </div>
    )
}

export default StoreItemContent;