import { IAttributeCreateDTO } from "../dto/attribute/IAttributeCreateDTO";
import { IAttributeDTO } from "../dto/attribute/IAttributeDTO";
import { BaseEntityService } from "./baseEntityService";

export class AttributeService extends BaseEntityService<IAttributeDTO, IAttributeDTO, IAttributeCreateDTO, IAttributeDTO> {
    constructor() {
        super('attributes');   
    }
}