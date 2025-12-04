import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import EntityProperty from "../../table/EntityProperty";

const Category = (props: {entity: ICategoryDTO}) => {
    return (
        <EntityProperty name="Category Name" value={props.entity.categoryName} isEven={true}/>
    );
}

export default Category;