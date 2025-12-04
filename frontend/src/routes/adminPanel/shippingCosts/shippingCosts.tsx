import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import ShippingCostDetails from "./shippingCostDetails";
import { IShippingCostDTO } from "../../../dto/shippingCost/IShippingCostDTO";
import { ShippingCostService } from "../../../services/shippingCostService";
import ShippingCostListItem from "../../../components/entity/shippingCosts/ShippingCostListItem";
import { PackageSizeService } from "../../../services/packageSizeService";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";
import TableSearchSelect from "../../../components/table/TableSearchSelect";

const ShippingCosts = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const shippingCostService = new ShippingCostService();
    const packageSizeService = new PackageSizeService();
    const shippingMethodService = new ShippingMethodService();

    const [data, setData] = useState([] as IShippingCostDTO[]);
    const [filterData, setFilterData] = useState({
        packageSizes: [] as IPackageSizeDTO[],
        shippingMethods: [] as IShippingMethodDTO[]
    });
    const [filters, setFilters] = useState({
        packageSizeFilter: "",
        shippingMethodFilter: ""
    });

    useEffect(() => {  
        fetchAll(); 
        fetchFilterData();
    }, [jwtData]);

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await shippingCostService.delete(id, jwtData);
        if (response) fetchAll();
    }

    const fetchAll = async () => {
        if (!jwtData) return;

        let response = await shippingCostService.getAll(jwtData);
        setData(response ? response.sort((a, b) => a.costPerUnit > b.costPerUnit ? 1 : -1) : []);
    }

    const setFilterValues = (value: string, type?: number) => {
        setFilters({
            packageSizeFilter: type === 0 ? value : filters.packageSizeFilter,
            shippingMethodFilter: type === 1 ? value : filters.shippingMethodFilter
        })
    }

    const fetchFilterData = async() => {
        let packageSizeResponse = await packageSizeService.getAll();
        let shippingMethodResponse = await shippingMethodService.getAll();

        if (packageSizeResponse && shippingMethodResponse) {
            setFilterData({
                packageSizes: packageSizeResponse,
                shippingMethods: shippingMethodResponse
            })
        }
    }

    const getFilteredData = (data: IShippingCostDTO[]) => {
        return data.filter(s =>
            s.packageSize.includes(filters.packageSizeFilter) &&
            s.shippingMethod.includes(filters.shippingMethodFilter)
        );
    }

    let filteredData = getFilteredData(data);

    let packageSizeSelectValues = filterData
    ? filterData.packageSizes.map(p => ({name: p.sizeName, value: p.sizeName}))
    : [];

    let shippingMethodSelectValues = filterData
    ? filterData.shippingMethods.map(s => ({name: s.methodName, value: s.methodName}))
    : [];

    let shippingCosts = [];
    for (let index = 0; index < filteredData.length; index++) {
        shippingCosts.push(<ShippingCostListItem key={index} index={index} entity={filteredData[index]} onDelete={onDelete}/>);
    }

    if (id) {
        return (
            <ShippingCostDetails onDelete={onDelete}/>
        );
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Shipping Costs" nav="create" btn="New"/>
            <div className="row mt-2">
                <div className="col-6 col-lg-4">
                    <TableSearchSelect label="Package Size" selectValues={packageSizeSelectValues} type={0} setFilter={setFilterValues}/>
                </div>
                <div className="col-6 col-lg-4">
                    <TableSearchSelect label="Shipping Method" selectValues={shippingMethodSelectValues} type={1} setFilter={setFilterValues}/>
                </div>
            </div>
            <div className="row table-head">
                <div className="col-3">
                    Package Size
                </div>
                <div className="col-4">
                    Shipping Method
                </div>
                <div className="col-3 d-none d-lg-block">
                    Cost Per Unit
                </div>
                <div className="col-2 d-lg-none">
                    Cost/u
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {shippingCosts}
        </div>
    );
}

export default ShippingCosts;