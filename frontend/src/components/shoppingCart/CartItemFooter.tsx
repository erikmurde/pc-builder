import { Collapse } from "react-bootstrap";
import { useState } from "react";
import { Link } from "react-router-dom";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import PcComponentCard from "../card/PcComponentCard";

interface IProps {
    entity: ICartPcDTO,
    costPerUnit: number
}

const CartItemFooter = (props: IProps) => {
    const [open, setOpen] = useState(false);
    let pcBuild = props.entity.pcBuild;
    let components: JSX.Element[] = [];

    // Show if PC does not include a hard drive
    if (!pcBuild.pcComponents.filter(c => c.categoryName === "Hard Drive")[0]) { 
        components.push(<PcComponentCard key={props.entity.id + "-" + "None"} pcDiscount={pcBuild.discountPercentage} entity={{
            categoryName: "Hard Drive",
            componentName: "None", 
            imageSrc: "https://www.cyberpowerpc.com/images/None_Selected_220.png",
            discountPercentage: 0, price: 0, stock: 0, componentId: ""
        }}/>);
    }

    pcBuild.pcComponents
        .sort((a, b) => a.categoryName > b.categoryName ? 1 : -1);

    pcBuild.pcComponents.forEach(component => {
        components.push(<PcComponentCard key={props.entity.id + "-" + component.componentName} 
            entity={component} pcDiscount={pcBuild.discountPercentage}/>);
    });

    return (
        <> 
            <div className="row flex-center cart-item-header p-2">
                <div className="col-6 text-start">
                    <Link
                        role="button"
                        className="text-decoration-none"
                        aria-expanded={open}
                        aria-controls={props.entity.id}
                        onClick={() => setOpen(!open)} 
                        to="">
                        {open ? "hide details" : "show details"}
                    </Link>                    
                </div>
                <div className="col-6 text-end">
                    Item Total - <strong>${Math.round(props.costPerUnit * props.entity.qty * 100) / 100}</strong>
                </div>
                <hr className="cart-hr"/>
            </div>
            <Collapse in={open}>
                <div id={props.entity.id}>
                    {components}
                </div>
            </Collapse>
        </>
    )
}

export default CartItemFooter;