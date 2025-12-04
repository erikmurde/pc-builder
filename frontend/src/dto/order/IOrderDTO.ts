export interface IOrderDTO {
    id: string,
    userEmail: string,
    status: string,
    discountPercentage: number,
    orderNr: string,
    orderPlacedAt: Date,
    orderCompletedAt: Date | null,
    orderCancelledAt: Date | null,
    totalShippingCost: number,
    totalPcCost: number
}