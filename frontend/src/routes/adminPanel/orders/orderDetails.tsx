import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { OrderService } from "../../../services/orderService";
import { IOrderDetailsDTO } from "../../../dto/order/IOrderDetailsDTO";
import { JwtContext } from "../../root";
import OrderPcListItem from "../../../components/entity/orderPcs/OrderPcListItem";
import Order from "../../../components/entity/orders/Order";
import PaymentSimpleListItem from "../../../components/entity/payments/PaymentSimpleListItem";

const OrderDetails = () => {
    const service = new OrderService();
    const { jwtData } = useContext(JwtContext);
    const [data, setData] = useState({} as IOrderDetailsDTO);
    const { id } = useParams();

    useEffect(() => {
        fetchOrder();
    }, [jwtData]);

    const fetchOrder = async() => {
        if (!id || !jwtData) return;

        let order = await service.getEntity(id, jwtData);
        if (order) setData(order);
    }

    let orderPcs = [];
    if (data.orderPcs) {
        for (let index = 0; index < data.orderPcs.length; index++) {
            orderPcs.push(<OrderPcListItem key={index} index={index} entity={data.orderPcs[index]}/>);
        }
    }
    let payments = [];
    if (data.payments) {
        for (let index = 0; index < data.payments.length; index++) {
            payments.push(<PaymentSimpleListItem key={index} index={index} entity={data.payments[index]}/>);
        }
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Order Details" nav="../orders" btn="Back"/>
            <div className="row table-head">
                <div className="col-10">
                    General Details
                </div>
                <div className="col-2 text-center">
                    <Link to={"../orders/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                </div>
            </div>
            <Order entity={data}/>
            <div className="row table-head">
                <div className="col-4 col-lg-6">
                    PC Name
                </div>
                <div className="col-3 col-lg-2">
                    Package Size
                </div>
                <div className="col-2 d-none d-lg-block">
                    Price Per Unit
                </div>
                <div className="col-3 d-lg-none">
                    Price/u
                </div>
                <div className="col-2">
                    Qty
                </div>
            </div>
            {orderPcs}
            <br/>
            <div className="row table-head">
                <div className="col-4 col-xl-5">
                    Payment Nr
                </div>
                <div className="col-4 col-xl-3">
                    Amount Paid
                </div>
                <div className="col-4">
                    Payment Date
                </div>
            </div>
            {payments}
        </div>
    );
}

export default OrderDetails;