import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { OrderService } from "../../../services/orderService";
import { IOrderDTO } from "../../../dto/order/IOrderDTO";
import { useNavigate, useParams } from "react-router-dom";
import ProfileOrderItem from "../../../components/profile/order/ProfileOrderItem";
import { IdentityService } from "../../../services/identityService";
import UserOrderDetails from "./UserOrderDetails";
import OrderHistoryView from "./views/OrderHistoryView";
import { IOrderEditDTO } from "../../../dto/order/IOrderEditDTO";
import { StatusService } from "../../../services/statusService";

const OrderHistory = () => {
    const orderService = new OrderService();
    const identityService = new IdentityService();
    const statusService = new StatusService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const [data, setData] = useState([] as IOrderDTO[]);
    const [statusId, setStatusId] = useState("");

    useEffect(() => { 
        if (!jwtData) return navigate("../login");
        getAll(); 
        getStatus();
    }, [jwtData]);

    const getAll = async () => {
        let response = await orderService.getAll(jwtData!);

        if (response) {
            let jwtObject = identityService.getJwtObject(jwtData!);
            setData(response.filter(o => o.userEmail === jwtObject.email));
        }
    }

    const getStatus = async() => {
        let response = await statusService.getAll();
        if (response) setStatusId(response.filter(s => s.statusName === "Cancelled")[0].id);
    }

    const onSubmit = async(values: IOrderEditDTO) => {
        if (!jwtData) return;

        let response = await orderService.edit(values.id, values, jwtData);
        if (response) getAll();
    }

    let activeOrders: JSX.Element[] = [];
    let completedOrders: JSX.Element[] = [];
    let cancelledOrders: JSX.Element[] = [];

    data.forEach(order => {
        if (order.status === "Completed") {
            completedOrders.push(<ProfileOrderItem key={order.id} entity={order} 
                statusId={statusId} onSubmit={onSubmit}/>);
        } else if (order.status === "Cancelled") {
            cancelledOrders.push(<ProfileOrderItem key={order.id} entity={order} 
                statusId={statusId} onSubmit={onSubmit}/>);
        } else {
            activeOrders.push(<ProfileOrderItem key={order.id} entity={order} 
                statusId={statusId} onSubmit={onSubmit}/>);
        }
    });

    if (id) {
        return <UserOrderDetails />;
    }

    return (
        <OrderHistoryView activeOrders={activeOrders} completedOrders={completedOrders} cancelledOrders={cancelledOrders}/>
    );
}

export default OrderHistory;