import { IPackageSizeCreateDTO } from "../dto/packageSize/IPackageSizeCreateDTO";
import { IPackageSizeDTO } from "../dto/packageSize/IPackageSizeDTO";
import { BaseEntityService } from "./baseEntityService";

export class PackageSizeService extends BaseEntityService<IPackageSizeDTO, IPackageSizeDTO, IPackageSizeCreateDTO, IPackageSizeDTO> {
    constructor() {
        super('packageSizes');   
    }
}