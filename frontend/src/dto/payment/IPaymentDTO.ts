export interface IPaymentDTO {
    id: string,
    userEmail: string,
    orderNr: string,
    paymentNr: string,
    amountPaid: number,
    paymentDate: Date
}