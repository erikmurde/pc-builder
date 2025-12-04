import { IOrderPcCreateDTO } from "../orderPc/IOrderPcCreateDTO";
import { IOrderShippingCostCreateDTO } from "../orderShippingCost/IOrderShippingCostCreateDTO";
import { IPaymentCreateDTO } from "../payment/IPaymentCreateDTO";

export interface IOrderCreateDTO {
    statusId: string,
    discountId: string,
    shippingMethodId: string,
    orderNr: string,
    customerName: string,
    customerPhoneNumber: string,
    shippingAddress: string,
    shippingPostalCode: string,
    comment?: string,

    orderPcData: IOrderPcCreateDTO[],
    paymentData: IPaymentCreateDTO[],
    orderShippingCostData: IOrderShippingCostCreateDTO[]
}