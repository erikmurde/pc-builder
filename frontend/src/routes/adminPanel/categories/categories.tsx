import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { CategoryService } from "../../../services/categoryService";
import { useParams } from "react-router-dom";
import CategoryDetails from "./categoryDetails";
import FormHeader from "../../../components/form/FormHeader";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import CategoryListItem from "../../../components/entity/categories/CategoryListItem";

const Categories = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new CategoryService();
    const [data, setData] = useState([] as ICategoryDTO[]);

    useEffect(() => {  
        getAll(); 
    }, [jwtData]);

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await service.delete(id, jwtData);
        if (response) getAll();
    }

    const getAll = async () => {
        let response = await service.getAll();
        setData(response ? response : []);
    }

    if (id) {
        return (
            <CategoryDetails onDelete={onDelete}/>
        );
    }

    let categories = [];
    for (let index = 0; index < data.length; index++) {
        categories.push(<CategoryListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Categories" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    Category Name
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {categories}
        </div>
    );
}

export default Categories;