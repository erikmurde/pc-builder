export interface IPaymentDetailsDTO {
    id: string,
    customerName: string,
    userEmail: string,
    orderNr: string,
    paymentNr: string,
    amountPaid: number,
    paymentDate: Date,
    comment?: string
}