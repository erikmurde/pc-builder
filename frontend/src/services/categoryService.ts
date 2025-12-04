import { ICategoryCreateDTO } from "../dto/category/ICategoryCreateDTO";
import { ICategoryDTO } from "../dto/category/ICategoryDTO";
import { BaseEntityService } from "./baseEntityService";

export class CategoryService extends BaseEntityService<ICategoryDTO, ICategoryDTO, ICategoryCreateDTO, ICategoryDTO> {
    constructor() {
        super('categories');   
    }

    async getCustomCategory(): Promise<ICategoryDTO | undefined> {
        let categories = await this.getAll();

        if (!categories) return undefined;

        return categories.filter(c => c.categoryName === "Custom PC")[0];
    }
}