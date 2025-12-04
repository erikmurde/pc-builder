import { ICartPcDTO } from "../dto/cartPc/ICartPcDTO";
import { IOrderCreateDTO } from "../dto/order/IOrderCreateDTO";
import { IOrderDTO } from "../dto/order/IOrderDTO";
import { IOrderDetailsDTO } from "../dto/order/IOrderDetailsDTO";
import { IOrderEditDTO } from "../dto/order/IOrderEditDTO";
import { IOrderShippingCostCreateDTO } from "../dto/orderShippingCost/IOrderShippingCostCreateDTO";
import { IPackageSizeDTO } from "../dto/packageSize/IPackageSizeDTO";
import { IShippingCostDTO } from "../dto/shippingCost/IShippingCostDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";
import { IComponentDetailsDTO } from "../dto/component/IComponentDetailsDTO";
import { IDiscountDTO } from "../dto/discount/IDiscountDTO";
import { IShippingMethodDTO } from "../dto/shippingMethod/IShippingMethodDTO";
import { IStatusDTO } from "../dto/status/IStatusDTO";
import { ICheckoutFormValues } from "../routes/checkout/CheckoutView";
import { IOrderPcCreateDTO } from "../dto/orderPc/IOrderPcCreateDTO";

export class OrderService extends BaseEntityServiceEdit<IOrderDTO, IOrderDetailsDTO, IOrderCreateDTO, IOrderEditDTO> {
    constructor() {
        super('orders');   
    }

    createOrder = (formValues: ICheckoutFormValues, data: {
        cartPcs: ICartPcDTO[],
        statuses: IStatusDTO[],
        packageSizes: IPackageSizeDTO[],
        shippingMethods: IShippingMethodDTO[],
        shippingCosts: IShippingCostDTO[]
        discounts: IDiscountDTO[],
        pcCases: IComponentDetailsDTO[]}): IOrderCreateDTO => {

        let orderValues: IOrderCreateDTO = {
            statusId: data.statuses.filter(s => s.statusName === "Placed")[0].id,
            discountId: this.getDiscountId(data.discounts),
            shippingMethodId: formValues.shippingMethodId,
            orderNr: `ON${this.getRandomNumber()}`,
            customerName: `${formValues.firstName} ${formValues.lastName}`,
            customerPhoneNumber: formValues.phoneNumber.toString(),
            shippingAddress: `${formValues.streetAddress}, ${formValues.city}`,
            shippingPostalCode: formValues.zipCode.toString(),
            comment: formValues.comment ?? undefined,
        
            orderPcData: [],
            paymentData: [],
            orderShippingCostData: []
        }

        // Cost of all items + shipping
        let totalCost = 0;

        data.cartPcs.forEach(cartPc => {
            let caseId = cartPc.pcBuild.pcComponents
                .filter(c => c.categoryName === "Case")[0].componentId;

            let packageSize = this.getPackageSize(data.packageSizes, data.pcCases.filter(c => c.id === caseId)[0]);
            if (!packageSize) return;

            let shippingMethod = data.shippingMethods
                .filter(s => s.id === formValues.shippingMethodId)[0];

            // Create orderPcs and orderShippingCosts
            let orderPc = this.createOrderPc(cartPc, packageSize);
            let shippingCost = this.createOrderShippingCost(
                data.shippingCosts, packageSize.sizeName, shippingMethod.methodName, orderPc.qty);

            // Add orderPcs and orderShippingCosts
            orderValues.orderPcData.push(orderPc);
            orderValues.orderShippingCostData.push(shippingCost);

            totalCost += orderPc.pricePerUnit * orderPc.qty + shippingCost.totalCost;
        });

        // Factor in discount
        totalCost *= (1 - Number(data.discounts
            .filter(d => d.id === orderValues.discountId)[0].discountPercentage) / 100);

        // Add payment
        orderValues.paymentData.push({
            paymentNr: `PN${this.getRandomNumber()}`, 
            amountPaid: Math.round(totalCost * 100) / 100
        });

        return orderValues;
    } 

    createOrderPc = (cartPc: ICartPcDTO, packageSize: IPackageSizeDTO): IOrderPcCreateDTO => {
        let pcCost = Math.round(cartPc.pcBuild.pcComponents
            .reduce((sum, c) => sum + Number(c.price * (1 - c.discountPercentage / 100)), 0) 
            * (1 - cartPc.pcBuild.discountPercentage / 100) * 100) / 100;

        return {
            pcBuildId: cartPc.pcBuild.id,
            packageSizeId: packageSize.id,
            pricePerUnit: pcCost,
            qty: cartPc.qty
        }
    }

    createOrderShippingCost = (
        shippingCosts: IShippingCostDTO[], packageSize: string, 
        shippingMethod: string, qty: number): IOrderShippingCostCreateDTO => {
    
        let shippingCost = shippingCosts.filter(s => 
            s.packageSize === packageSize && 
            s.shippingMethod === shippingMethod)[0];

        return {
            shippingCostId: shippingCost.id,
            totalCost: Math.round(Number(shippingCost.costPerUnit) * qty * 100) / 100
        }
    }

    getPackageSize = (packageSizes: IPackageSizeDTO[], pcCase: IComponentDetailsDTO): IPackageSizeDTO => {
        let caseType = pcCase.componentAttributes.filter(c => c.attributeName === "Type")[0].attributeValue;
        let size: string;

        if (caseType.includes("Mini")) size = "Small";
        else if (caseType.includes("Mid")) size = "Medium";
        else size = "Large";
    
        return packageSizes.filter(p => p.sizeName === size)[0];
    }

    // TODO How to decide order discounts ???
    getDiscountId = (discounts: IDiscountDTO[]): string => 
        discounts.filter(d => Number(d.discountPercentage) === 0)[0].id;

    getRandomNumber = (): number => 
        Math.floor((Math.random() * 9 + 1) * Math.pow(10, 9));
}