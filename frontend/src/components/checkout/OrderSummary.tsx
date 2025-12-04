import { FormikProps } from "formik";
import FormHeader from "../form/FormHeader";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import OrderItem from "./OrderItem";
import OrderSummaryField from "./OrderSummaryField";
import { OrderService } from "../../services/orderService";
import { IPackageSizeDTO } from "../../dto/packageSize/IPackageSizeDTO";
import { IShippingCostDTO } from "../../dto/shippingCost/IShippingCostDTO";
import { IShippingMethodDTO } from "../../dto/shippingMethod/IShippingMethodDTO";
import { useEffect, useState } from "react";
import { ComponentService } from "../../services/componentService";

interface IProps {
    form: FormikProps<any>,
    cartPcs: ICartPcDTO[],
    selectValues: {
        shippingMethods: IShippingMethodDTO[],
        shippingCosts: IShippingCostDTO[],
        packageSizes: IPackageSizeDTO[]
    }
}

const OrderSummary = (props: IProps) => {
    const componentService = new ComponentService();
    const orderService = new OrderService();
    const [packageSizes, setPackageSizes] = useState([] as {id: string, sizeName: string}[]);

    // Get the appropriate package size for each order item.
    useEffect(() => {
        props.cartPcs.forEach(async cartPc => {
            let caseId = cartPc.pcBuild.pcComponents
                .filter(c => c.categoryName === "Case")[0].componentId;

            let pcCase = await componentService.getEntity(caseId);
            if (!pcCase) return;

            let packageSize = orderService.getPackageSize(props.selectValues.packageSizes, pcCase);
            if (packageSize) setPackageSizes(sizes => [...sizes, {id: cartPc.id, sizeName: packageSize.sizeName}]);
        })
    }, [props.cartPcs, props.selectValues]);

    let orderItems: JSX.Element[] = [];
    let subTotal = 0;
    let shippingCost = 0;

    let shippingMethod = props.selectValues.shippingMethods
        .filter(s => s.id === props.form.values.shippingMethodId)[0];

    // Calculate the cost of each order item and add to the subtotal
    props.cartPcs.forEach(cartPc => {
        let itemCost = cartPc.pcBuild.pcComponents
        .reduce((sum, c) => sum + Number(c.price 
        * (1 - c.discountPercentage / 100)), 0) 
        * (1 - cartPc.pcBuild.discountPercentage / 100)
        * cartPc.qty;

        subTotal += itemCost;
        orderItems.push(<OrderItem key={cartPc.id} entity={cartPc} itemCost={itemCost}/>);
    })

    // Calculate the shipping cost for each order item
    if (shippingMethod && packageSizes.length > 0) {
        props.cartPcs.forEach(cartPc => {       
            let packageSize = packageSizes.filter(p => p.id === cartPc.id)[0];

            shippingCost += orderService.createOrderShippingCost(
                props.selectValues.shippingCosts, 
                packageSize.sizeName, 
                shippingMethod.methodName, 
                cartPc.qty).totalCost;
        })
    }

    return (
        <div className="col-10 col-xl-4 text-center" id="order-summary">
            <FormHeader title="Order Summary"/>
            <div className="row p-2 text-start">
                <h5 className="mb-0 mt-2">Items</h5>
            </div>
            <hr className="mt-0 mb-2"/>
            {orderItems}
            <hr className="mt-2 mb-2"/>
            <OrderSummaryField name="Subtotal" value={Math.round(subTotal * 100) / 100}/>
            <OrderSummaryField name="Shipping" value={Math.round(shippingCost * 100) / 100}/>
            <hr className="mt-2 mb-2"/>
            <OrderSummaryField name="Total Cost" value={Math.round((subTotal + shippingCost) * 100) / 100} strong/>
            <div className={"row text-center mb-3" + (props.form.status === "Invalid" ? " d-flex" : " d-none")}>
                <span className="text-danger">Unable to place order - Insufficient stock</span>
            </div>
            <div className="row flex-center mt-2 mb-2">
                <div className="col-4 col-xl-6">
                    <button type="submit" className="btn btn-primary card-button">
                        <h5 className="mb-0">Place Order</h5>
                    </button>
                </div>
            </div>
        </div>
    );
}

export default OrderSummary;