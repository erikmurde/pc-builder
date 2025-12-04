import { IStatusCreateDTO } from "../dto/status/IStatusCreateDTO";
import { IStatusDTO } from "../dto/status/IStatusDTO";
import { BaseEntityService } from "./baseEntityService";

export class StatusService extends BaseEntityService<IStatusDTO, IStatusDTO, IStatusCreateDTO, IStatusCreateDTO> {
    constructor() {
        super('statuses');   
    }
}