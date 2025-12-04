import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { OrderService } from "../../../services/orderService";
import { useNavigate, useParams } from "react-router-dom";
import { IOrderDetailsDTO } from "../../../dto/order/IOrderDetailsDTO";
import Order from "../../../components/entity/orders/Order";
import OrderPcListItem from "../../../components/entity/orderPcs/OrderPcListItem";
import PaymentSimpleListItem from "../../../components/entity/payments/PaymentSimpleListItem";

const UserOrderDetails = () => {
    const service = new OrderService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const [data, setData] = useState({} as IOrderDetailsDTO);

    useEffect(() => { 
        if (!jwtData || !id) return navigate("../login");
        fetchOrder(); 
    }, [jwtData]);

    const fetchOrder = async () => {
        let response = await service.getEntity(id!, jwtData!);
        if (response) setData(response);
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
        <>
            <div className="row flex-center table-head m-0 p-2">
                <div className="col-12">
                    <h2>Order Details</h2>
                </div>
            </div>
            <div className="row flex-center m-0 p-2">
                <div className="col-12">
                    <Order entity={data}/>
                    <div className="row table-head">
                        <div className="col-4 col-lg-6">
                            PC Name
                        </div>
                        <div className="col-2 d-none d-xxl-block">
                            Package Size
                        </div>
                        <div className="col-3 col-lg-2 d-xxl-none">
                            Package
                        </div>
                        <div className="col-2 d-none d-xxl-block">
                            Price Per Unit
                        </div>
                        <div className="col-3 col-lg-2 d-xxl-none">
                            Price/unit
                        </div>
                        <div className="col-2">
                            Qty
                        </div>
                    </div>
                    {orderPcs}
                    <br/>
                    <div className="row table-head">
                        <div className="col-4 col-lg-5">
                            Payment Nr
                        </div>
                        <div className="col-4 col-lg-3">
                            Amount Paid
                        </div>
                        <div className="col-4">
                            Payment Date
                        </div>
                    </div>
                    {payments}
                </div>
            </div>
        </>
    );
}

export default UserOrderDetails