import { IShippingCostCreateDTO } from "../dto/shippingCost/IShippingCostCreateDTO";
import { IShippingCostDTO } from "../dto/shippingCost/IShippingCostDTO";
import { IShippingCostEditDTO } from "../dto/shippingCost/IShippingCostEditDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";

export class ShippingCostService extends BaseEntityServiceEdit<IShippingCostDTO, IShippingCostDTO, IShippingCostCreateDTO, IShippingCostEditDTO> {
    constructor() {
        super('shippingCosts');   
    }
}