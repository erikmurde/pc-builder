import { IComponentAttributeCreateDTO } from "../dto/componentAttribute/IComponentAttributeCreateDTO";
import { IComponentAttributeDTO } from "../dto/componentAttribute/IComponentAttributeDTO";
import { IComponentAttributeEditDTO } from "../dto/componentAttribute/IComponentAttributeEditDTO";
import { BaseEntityServiceEdit } from "./baseEntityServiceEdit";

export class ComponentAttributeService 
    extends BaseEntityServiceEdit<IComponentAttributeDTO, IComponentAttributeDTO, IComponentAttributeCreateDTO, IComponentAttributeEditDTO> {

    constructor() {
        super('componentAttributes');   
    }
}