import { IShippingMethodCreateDTO } from "../dto/shippingMethod/IShippingMethodCreateDTO";
import { IShippingMethodDTO } from "../dto/shippingMethod/IShippingMethodDTO";
import { BaseEntityService } from "./baseEntityService";

export class ShippingMethodService extends BaseEntityService<IShippingMethodDTO, IShippingMethodDTO, IShippingMethodCreateDTO, IShippingMethodDTO> {
    constructor() {
        super('shippingMethods');   
    }
}