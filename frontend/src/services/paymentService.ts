import { IPaymentDTO } from "../dto/payment/IPaymentDTO";
import { IPaymentDetailsDTO } from "../dto/payment/IPaymentDetailsDTO";
import { IPaymentEditDTO } from "../dto/payment/IPaymentEditDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";

export class PaymentService extends BaseEntityServiceEdit<IPaymentDTO, IPaymentDetailsDTO, IPaymentEditDTO, IPaymentEditDTO> {
    constructor() {
        super('payments');   
    }
}