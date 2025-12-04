import { useEffect, useState } from "react";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import { ICartPcEditDTO } from "../../dto/cartPc/ICartPcEditDTO";
import EntityImageLarge from "../image/EntityImageLarge";
import CartItemFooter from "./CartItemFooter";
import CartItemHeader from "./CartItemHeader";
import CartSummaryItem from "./CartSummaryItem";
import { Form, Formik } from "formik";

interface IProps {
    entity: ICartPcDTO,
    onDelete: (id: string) => void,
    onEdit: (id: string, pcBuildId: string) => void,
    validate: (values: ICartPcEditDTO) => void,
    onSubmit: (values: ICartPcEditDTO) => void
}

const CartItem = (props: IProps) => {
    const [initialValues, setInitialValues] = useState({id: "", pcBuildId: "", qty: ""});

    useEffect(() => {
        setInitialValues({
            id: props.entity.id, 
            pcBuildId: props.entity.pcBuild.id,
            qty: props.entity.qty.toString()
        });
    }, []);

    let pcBuild = props.entity.pcBuild;

    let costPerUnit = Math.round(pcBuild.pcComponents
        .reduce((sum, c) => sum + Number(c.price 
            * (1 - c.discountPercentage / 100)), 0) 
            * (1 - pcBuild.discountPercentage / 100) * 100) / 100;

    let editBtn = pcBuild.isCustom 
        ?   <div className="col">
                <button 
                    type="button"
                    className="btn btn-outline-primary cart-btn" 
                    onClick={() => props.onEdit(props.entity.id, props.entity.pcBuild.id)}>
                    Edit
                </button>
            </div>
        : <></>

    return (
        <Formik
            initialValues={initialValues}
            validate={(values) => props.validate(values)}
            onSubmit={(values) => props.onSubmit(values)}
            enableReinitialize={true}>
            {(form => <Form>
                <CartItemHeader pcName={pcBuild.pcName} costPerUnit={costPerUnit} form={form}/>
                <div className="row cart-item-row flex-center">
                    <EntityImageLarge src={pcBuild.imageSrc} isNotRow={true} alt="Image of a gaming PC"/>
                    <div className="col-7 text-start">
                        <CartSummaryItem title="Case" category="Case" components={pcBuild.pcComponents}/>
                        <CartSummaryItem title="CPU" category="Processor" components={pcBuild.pcComponents}/>
                        <CartSummaryItem title="GPU" category="Graphics Card" components={pcBuild.pcComponents}/>
                        <CartSummaryItem title="Memory" category="Memory" components={pcBuild.pcComponents}/>
                        <CartSummaryItem title="Storage" category="Solid State Drive" components={pcBuild.pcComponents}/>
                    </div>
                    <div className="col"></div>
                    <div className="col-8 col-lg-6 col-xl-2">
                        <div className="col mb-2 mt-2 mt-xl-0">
                            <button 
                                type="button"
                                className="btn btn-outline-primary cart-btn" 
                                onClick={() => props.onDelete(props.entity.id)}>
                                Remove
                            </button>
                        </div>
                        {editBtn}
                        <div className="col mt-2">
                            <button
                                type="submit" 
                                className="btn btn-outline-primary cart-btn">
                                Update
                            </button>
                        </div>
                    </div>
                </div>
                <CartItemFooter entity={props.entity} costPerUnit={costPerUnit}/>
            </Form>)}
        </Formik>
    )
}

export default CartItem;