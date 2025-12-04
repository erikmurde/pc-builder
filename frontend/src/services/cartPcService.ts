import { ICartPcCreateDTO } from "../dto/cartPc/ICartPcCreateDTO";
import { ICartPcDTO } from "../dto/cartPc/ICartPcDTO";
import { ICartPcEditDTO } from "../dto/cartPc/ICartPcEditDTO";
import { BaseEntityService } from "./baseEntityService";

export class CartPcService extends BaseEntityService<ICartPcDTO, ICartPcDTO, ICartPcCreateDTO, ICartPcEditDTO> {
    constructor() {
        super('cartPcs');   
    }

    checkStock = (cartPc: ICartPcDTO): boolean => {
        if (!cartPc.pcBuild.isCustom) {
            return cartPc.pcBuild.stock >= cartPc.qty;
        }

        return cartPc.pcBuild.pcComponents
            .filter(c => c.stock < cartPc.qty).length === 0;
    }
}