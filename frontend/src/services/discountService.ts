import { IDiscountCreateDTO } from "../dto/discount/IDiscountCreateDTO";
import { IDiscountDTO } from "../dto/discount/IDiscountDTO";
import { BaseEntityService } from "./baseEntityService";

export class DiscountService extends BaseEntityService<IDiscountDTO, IDiscountDTO, IDiscountCreateDTO, IDiscountDTO> {
    constructor() {
        super('discounts');   
    }
}