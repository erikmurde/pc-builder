import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { ComponentService } from "../../../services/componentService";
import { IComponentDTO } from "../../../dto/component/IComponentDTO";
import ComponentDetails from "./componentDetails";
import ComponentListItem from "../../../components/entity/components/ComponentListItem";
import { CategoryService } from "../../../services/categoryService";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import TableSearchSelect from "../../../components/table/TableSearchSelect";
import TableSearchText from "../../../components/table/TableSearchText";

const Components = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const componentService = new ComponentService();
    const categoryService = new CategoryService();

    const [data, setData] = useState([] as IComponentDTO[]);
    const [categoryData, setCategoryData] = useState([] as ICategoryDTO[]);
    const [filters, setFilters] = useState({
        categoryFilter: "",
        nameFilter: ""
    });

    useEffect(() => {  
        fetchAll(); 
        fetchCategories();
    }, [jwtData]);

    const fetchCategories = async() => {
        let response = await categoryService.getAll();
        setCategoryData(response ? response.filter(c => !c.categoryName.includes("PC")) : []);
    }

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await componentService.delete(id, jwtData);
        if (response) fetchAll();
    }

    const fetchAll = async () => {
        let response = await componentService.getAll();
        setData(response ? response.sort((a, b) => a.price > b.price ? 1 : -1) : []);
    }

    const setFilterValues = (value: string, type?: number) => {
        setFilters({
            categoryFilter: type === 0 ? value : filters.categoryFilter,
            nameFilter: type === 1 ? value : filters.nameFilter
        })
    }

    const filterData = (data: IComponentDTO[]) => {
        return data.filter(c => 
            c.categoryName.includes(filters.categoryFilter) &&
            c.componentName.toUpperCase().includes(filters.nameFilter.toUpperCase())
        );
    }

    let categorySelectValues = categoryData 
    ? categoryData.map(c => ({name: c.categoryName, value: c.categoryName}))
    : [];

    let filteredData = filterData(data);

    let components = [];
    for (let index = 0; index < filteredData.length; index++) {
        components.push(<ComponentListItem key={index} index={index} entity={filteredData[index]} onDelete={onDelete}/>);
    }


    if (id) {
        return (
            <ComponentDetails onDelete={onDelete}/>
        );
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Components" nav="create" btn="New"/>
            <div className="row mt-2">
                <div className="col-6 col-lg-4">
                    <TableSearchSelect label="Category" selectValues={categorySelectValues} type={0} setFilter={setFilterValues}/>
                </div>
                <div className="col-6 col-lg-4">
                    <TableSearchText label="Name" filterType={1} setFilter={setFilterValues}/>
                </div>
            </div>
            <div className="row table-head">
                <div className="col-3 d-none d-lg-block">
                    Component Name
                </div>
                <div className="col-4 d-lg-none">
                    Name
                </div>
                <div className="col d-none d-lg-block"></div>
                <div className="col-3 col-lg-2">
                    Category
                </div>
                <div className="col-2 d-none d-xl-block">
                    Discount
                </div>
                <div className="col-1 d-none d-lg-block d-xl-none">
                    <i className="fas fa-percentage"></i>
                </div>
                <div className="col-2 col-xl-1">
                    Price
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {components}
        </div>
    );
}

export default Components;