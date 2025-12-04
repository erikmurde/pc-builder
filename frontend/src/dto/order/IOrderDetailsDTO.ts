import { IOrderPcDTO } from "../orderPc/IOrderPcDTO";
import { IPaymentSimpleDTO } from "../payment/IPaymentSimpleDTO";

export interface IOrderDetailsDTO {
    id: string,
    userEmail: string,
    status: string,
    discountName: string,
    discountPercentage: number,
    shippingMethod: string,
    orderNr: string,
    orderPlacedAt: Date,
    orderCompletedAt: Date | null,
    orderCancelledAt: Date | null,
    customerName: string,
    customerPhoneNumber: string,
    shippingAddress: string,
    shippingPostalCode: string,
    totalShippingCost: number,
    comment?: string,
    orderPcs: IOrderPcDTO[],
    payments: IPaymentSimpleDTO[]
}