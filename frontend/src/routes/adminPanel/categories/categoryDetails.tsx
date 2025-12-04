import { useEffect, useState } from "react";
import { CategoryService } from "../../../services/categoryService";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import Category from "../../../components/entity/categories/Category";

const CategoryDetails = (props: {onDelete: (id: string) => void}) => {
    const { id } = useParams();
    const service = new CategoryService();
    const [data, setData] = useState({} as ICategoryDTO);

    useEffect(() => {
        fetchCategory();
    }, [id]);

    const fetchCategory = async() => {
        if (!id) return;

        let category = await service.getEntity(id);
        if (category) setData(category);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Category Details" nav="../categories" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../categories/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="category" nav="categories" onDelete={props.onDelete}/>
                </div>
            </div>
            <Category entity={data}/>
        </div>
    );
}

export default CategoryDetails;