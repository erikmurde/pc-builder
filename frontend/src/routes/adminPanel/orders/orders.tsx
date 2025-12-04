import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { OrderService } from "../../../services/orderService";
import { IOrderDTO } from "../../../dto/order/IOrderDTO";
import OrderDetails from "./orderDetails";
import OrderListItem from "../../../components/entity/orders/OrderListItem";
import TableSearchText from "../../../components/table/TableSearchText";
import { StatusService } from "../../../services/statusService";
import { IStatusDTO } from "../../../dto/status/IStatusDTO";
import TableSearchSelect from "../../../components/table/TableSearchSelect";

const Orders = () => {
    const orderService = new OrderService();
    const statusService = new StatusService();
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();

    const [data, setData] = useState([] as IOrderDTO[]);
    const [statusData, setStatusData] = useState([] as IStatusDTO[]);
    const [filters, setFilters] = useState({
        userNameFilter: "",
        statusFilter: ""
    });

    useEffect(() => { 
        if (!jwtData) return;

        fetchAll(); 
        fetchStatuses();
    }, [jwtData]);

    const fetchAll = async () => {
        let response = await orderService.getAll(jwtData!);
        setData(response ? response.sort((a, b) => a.orderPlacedAt > b.orderPlacedAt ? -1 : 1) : []);
    }

    const fetchStatuses = async() => {
        let response = await statusService.getAll();
        setStatusData(response ? response : []);
    }

    const setFilterValues = (value: string, type?: number) => {
        setFilters({
            userNameFilter: type === 0 ? value : filters.userNameFilter,
            statusFilter: type === 1 ? value : filters.statusFilter
        })
    }

    const filterData = (data: IOrderDTO[]) => {
        return data.filter(o => 
            o.userEmail.toUpperCase().includes(filters.userNameFilter.toUpperCase()) &&
            o.status.includes(filters.statusFilter)
        );
    }

    if (id) {
        return (
            <OrderDetails/>
        );
    }

    let statusSelectValues = statusData 
    ? statusData.map(c => ({name: c.statusName, value: c.statusName}))
    : [];

    let filteredData = filterData(data);

    let orders = [];
    for (let index = 0; index < filteredData.length; index++) {
        orders.push(<OrderListItem key={index} index={index} entity={filteredData[index]}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Orders" nav="create"/>
            <div className="row mt-2">
                <div className="col-6 col-lg-4">
                    <TableSearchSelect label="Status" selectValues={statusSelectValues} type={1} setFilter={setFilterValues}/>
                </div>
                <div className="col-6 col-lg-4">
                    <TableSearchText label="Username" filterType={0} setFilter={setFilterValues}/>
                </div>
            </div>
            <div className="row table-head">
            <div className="col-3 col-xl-2">
                    Order Nr
                </div>
                <div className="col-3 col-xl-2">
                    Placed By
                </div>
                <div className="col-2 col-xxl-3 d-none d-xl-block">
                    Placed At
                </div>
                <div className="col-2">
                    Status
                </div>
                <div className="col-2 d-none d-md-block">
                    Total Cost
                </div>
                <div className="col-2 d-md-none">
                    Cost
                </div>
                <div className="col-2 col-lg-1">
                    Actions
                </div>
            </div>
            {orders}
        </div>
    );
}

export default Orders;