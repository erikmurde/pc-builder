import { IPcBuildDetailsDTO } from "../../../dto/pcBuild/IPcBuildDetailsDTO";
import EntityImageLarge from "../../image/EntityImageLarge";
import EntityProperty from "../../table/EntityProperty";

const PcBuild = (props: {entity: IPcBuildDetailsDTO}) => {
    let componentCost = props.entity.pcComponents 
        ? props.entity.pcComponents.reduce((sum, c) => sum += c.price, 0)
        : 0;

    return (
        <>
            <EntityImageLarge src={props.entity.imageSrc} alt="Image of a PC build"/>
            <EntityProperty name="PC Name" value={props.entity.pcName} isEven={true}/>
            <EntityProperty name="Category" value={props.entity.categoryName} isEven={false}/>
            <EntityProperty 
                name="Cost Of Components"
                value={"$" + Math.round(componentCost * 100) / 100}
                isEven={true}/>
            <EntityProperty name="Discount Name" value={props.entity.discountName} isEven={false}/>
            <EntityProperty name="Discount Percent" value={props.entity.discountPercentage + "%"} isEven={true}/>
            <EntityProperty 
                name="Total Cost" 
                value={"$" + Math.round(componentCost * (1 - props.entity.discountPercentage / 100) * 100) / 100} 
                isEven={false}/>
            <EntityProperty name="Stock" value={props.entity.stock} isEven={true}/>
            <br/>
            <div className="row table-head">
                <div className="col-12">
                    Description
                </div>
            </div>
            <br/>
            <EntityProperty value={props.entity.description} isEven={false}/>
            <br/>
        </>
    );
}

export default PcBuild;