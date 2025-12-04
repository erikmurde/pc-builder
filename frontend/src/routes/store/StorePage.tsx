import { useState, useEffect } from "react";
import { PcBuildService } from "../../services/pcBuildService";
import { IPcBuildStoreDTO } from "../../dto/pcBuild/IPcBuildStoreDTO";
import StoreFilterGroup from "../../components/store/StoreFilterGroup";
import PrebuiltPcCard from "../../components/card/PrebuiltPcCard";
import StorePriceFilter from "../../components/store/StorePriceFilter";

export interface IFilter {
    value :string,
    label?: string
}

const StorePage = () => {
    const service = new PcBuildService();
    const [data, setData] = useState([] as IPcBuildStoreDTO[]);
    const [cpuFilters, setCpuFilters] = useState([] as string[]);
    const [gpuFilters, setGpuFilters] = useState([] as string[]);
    const [memoryFilters, setMemoryFilters] = useState([] as string[]);
    const [maxPrice, setMaxPrice] = useState(5000);

    useEffect(() => {  
        getAllPrebuilts(); 
    }, []);

    const getAllPrebuilts = async () => {
        let response = await service.getAllStore();
        setData(response ? response.sort((a, b) => (a.reviewScore > b.reviewScore) ? -1 : 1) : []);
    }

    const updateFilters = (checked: boolean, filter: string, type: number) => {
        if (checked) {
            if (type === 0) setCpuFilters(filters => [...filters, filter]);
            if (type === 1) setGpuFilters(filters => [...filters, filter]);
            if (type === 2) setMemoryFilters(filters => [...filters, filter]);
        } else {
            if (type === 0) setCpuFilters(filters => filters.filter(f => f != filter));
            if (type === 1) setGpuFilters(filters => filters.filter(f => f != filter));
            if (type === 2) setMemoryFilters(filters => filters.filter(f => f != filter));
        }
    }

    const clearFilters = () => {
        setCpuFilters([]);
        setGpuFilters([]);
        setMemoryFilters([]);
    }

    const getFilteredData = () => {
        let filtered = data.filter(pcBuild => 
            getCost(pcBuild) <= maxPrice && Number(pcBuild.stock) > 0);

        if (cpuFilters.length > 0) {
            filtered = filterData(filtered, cpuFilters);
        }
        if (gpuFilters.length > 0) {
            filtered = filterData(filtered, gpuFilters);
        }
        if (memoryFilters.length > 0) {
            filtered = filterData(filtered, memoryFilters);
        }
        return filtered;
    }

    const filterData = (data: IPcBuildStoreDTO[], filters: string[]) => {
        return data.filter(pcBuild =>
            filters.some(filter =>
                pcBuild.pcComponents.some(c =>
                    c.componentName.includes(filter)
        )));
    }

    const getCost = (pcBuild: IPcBuildStoreDTO) => {
        return pcBuild.pcComponents.reduce((sum, c) => 
        sum + c.price * (1 - c.discountPercentage / 100), 0)
        * (1 - pcBuild.discountPercentage / 100);
    }

    const cpuFilterValues: IFilter[] = [
        {value: "Intel Core i3"}, {value: "Intel Core i5"}, 
        {value: "Intel Core i7"}, {value: "Intel Core i9"}, 
        {value: "AMD Ryzen 5"}, {value: "AMD Ryzen 7"}
    ];

    const gpuFilterValues: IFilter[] = [
        {value: "GTX 16", label: "GTX 1600 Series"}, 
        {value: "RTX 30", label: "RTX 3000 Series"}, 
        {value: "RTX 40", label: "RTX 4000 Series"}, 
        {value: "Radeon RX 6", label: "Radeon RX 6000 Series"}, 
        {value: "Radeon RX 7", label: "Radeon RX 7000 Series"},
        {value: "Arc A7", label: "Intel Arc Series"}
    ]

    const memoryFilterValues: IFilter[] = [
        {value: "16GB"}, {value: "32GB"}, {value: "64GB"}, {value: "128GB"}
    ]

    const filteredData = getFilteredData();

    return (
        <div className="row m-0 align-items-start flex-row">
            <div className="col-12 col-lg-3 bg-white shadow mb-3 border border-primary">
                <StorePriceFilter maxPrice={maxPrice} setMaxPrice={setMaxPrice}/>
                <StoreFilterGroup title="Processor" filters={cpuFilterValues} type={0} updateFilters={updateFilters}/>
                <StoreFilterGroup title="Graphics Card" filters={gpuFilterValues} type={1} updateFilters={updateFilters}/>
                <StoreFilterGroup title="Memory" filters={memoryFilterValues} type={2} updateFilters={updateFilters}/>
            </div>
            <div className="col-12 col-lg-9">
                <div className="row">
                    {filteredData.map(pcBuild => 
                    <PrebuiltPcCard key={pcBuild.id} entity={pcBuild} clearFilters={clearFilters} minXxl={4} minXl={5}/>)}
                </div>
            </div>
        </div>
    );
}

export default StorePage;