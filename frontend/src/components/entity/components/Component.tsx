import { IComponentDetailsDTO } from "../../../dto/component/IComponentDetailsDTO";
import EntityImageLarge from "../../image/EntityImageLarge";
import EntityProperty from "../../table/EntityProperty";

const Component = (props: {entity: IComponentDetailsDTO}) => {
    return (
        <>
            <EntityImageLarge src={props.entity.imageSrc} alt="Image of a PC component"/>
            <EntityProperty name="Category" value={props.entity.categoryName} isEven={true}/>
            <EntityProperty name="Manufacturer" value={props.entity.manufacturerName} isEven={false}/>
            <EntityProperty name="Component Name" value={props.entity.componentName} isEven={true}/>
            <EntityProperty 
                name="Cost Without Discount"
                value={"$" + props.entity.price}
                isEven={false}/>
            <EntityProperty name="Discount Name" value={props.entity.discountName} isEven={true}/>
            <EntityProperty name="Discount Percent" value={props.entity.discountPercentage + "%"} isEven={false}/>
            <EntityProperty 
                name="Total Cost" 
                value={"$" + Math.round(parseFloat(props.entity.price) * (1 - props.entity.discountPercentage / 100) * 100) / 100} 
                isEven={true}/>
            <EntityProperty name="Stock" value={props.entity.stock} isEven={false}/>
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

export default Component;