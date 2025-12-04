import { IManufacturerCreateDTO } from "../dto/manufacturer/IManufacturerCreateDTO";
import { IManufacturerDTO } from "../dto/manufacturer/IManufacturerDTO";
import { BaseEntityService } from "./baseEntityService";

export class ManufacturerService extends BaseEntityService<IManufacturerDTO, IManufacturerDTO, IManufacturerCreateDTO, IManufacturerDTO> {
    constructor() {
        super('manufacturers');   
    }
}