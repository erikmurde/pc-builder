import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { PcBuildService } from "../../../services/pcBuildService";
import { IPcBuildDTO } from "../../../dto/pcBuild/IPcBuildDTO";
import PcBuildDetails from "./pcBuildDetails";
import PcBuildListItem from "../../../components/entity/pcBuilds/PcBuildListItem";
import { CategoryService } from "../../../services/categoryService";
import { ICategoryDTO } from "../../../dto/category/ICategoryDTO";
import TableSearchSelect from "../../../components/table/TableSearchSelect";
import TableSearchText from "../../../components/table/TableSearchText";

const PcBuilds = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const pcBuildService = new PcBuildService();
    const categoryService = new CategoryService();

    const [data, setData] = useState([] as IPcBuildDTO[]);
    const [categoryData, setCategoryData] = useState([] as ICategoryDTO[]);
    const [filters, setFilters] = useState({
        categoryFilter: "",
        nameFilter: "",
        maxStockFilter: ""
    });

    useEffect(() => {  
        if (!jwtData) return;

        fetchAll(); 
        fetchCategories();
    }, [jwtData]);

    const onDelete = async (id: string) => {
        let response = await pcBuildService.delete(id, jwtData!);
        if (response) fetchAll();
    }

    const fetchAll = async () => {
        let response = await pcBuildService.getAll();
        setData(response ? response : []);
    }

    const fetchCategories = async() => {
        let response = await categoryService.getAll();
        setCategoryData(response ? response.filter(c => c.categoryName.includes("PC")) : []);
    }

    const setFilterValues = (value: string, type?: number) => {
        setFilters({
            categoryFilter: type === 0 ? value : filters.categoryFilter,
            nameFilter: type === 1 ? value : filters.nameFilter,
            maxStockFilter: type === 2 ? value : filters.maxStockFilter
        })
    }

    const filterData = (data: IPcBuildDTO[]) => {
        return data.filter(c => 
            c.categoryName.includes(filters.categoryFilter) &&
            c.pcName.toUpperCase().includes(filters.nameFilter.toUpperCase()) &&
            (filters.maxStockFilter === "" || Number(c.stock) <= Number(filters.maxStockFilter))
        );
    }

    let categorySelectValues = categoryData 
    ? categoryData.map(c => ({name: c.categoryName, value: c.categoryName}))
    : [];

    let filteredData = filterData(data);

    let pcBuilds = [];
    for (let index = 0; index < filteredData.length; index++) {
        pcBuilds.push(<PcBuildListItem key={index} index={index} entity={filteredData[index]} onDelete={onDelete}/>);
    }

    if (id) {
        return (
            <PcBuildDetails onDelete={onDelete}/>
        );
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage PC Builds" nav="create" btn="New"/>
            <div className="row">
                <div className="col-8 col-xl-4 mt-2">
                    <TableSearchSelect label="Category" type={0} selectValues={categorySelectValues} setFilter={setFilterValues}/>
                </div>
                <div className="col-8 col-xl-4 mt-2">
                    <TableSearchText label="Name" filterType={1} setFilter={setFilterValues} />
                </div>
                <div className="col-8 col-xl-4 mt-2">
                    <TableSearchText label="Max Stock" inputType="number" filterType={2} setFilter={setFilterValues} />
                </div>
            </div>
            <div className="row table-head">
                <div className="col-3 col-lg-4">
                    PC Name
                </div>
                <div className="col-2">
                    Category
                </div>
                <div className="col-2">
                    Discount
                </div>
                <div className="col-2">
                    Stock
                </div>
                <div className="col-2">
                    Actions
                </div>
            </div>
            {pcBuilds}
        </div>
    );
}

export default PcBuilds;