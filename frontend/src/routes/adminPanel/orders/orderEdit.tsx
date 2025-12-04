import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import OrderEditFormView from "./views/orderEditFormView";
import { OrderService } from "../../../services/orderService";
import { IOrderEditDTO } from "../../../dto/order/IOrderEditDTO";
import { IStatusDTO } from "../../../dto/status/IStatusDTO"
import { StatusService } from "../../../services/statusService";

const OrderEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const orderService = new OrderService();
    const statusService = new StatusService();

    const [initialValues, setInitialValues] = useState({
        id: "",
        statusId: "",
        comment: ""
    });

    const [selectData, setData] = useState([] as IStatusDTO[]);

    useEffect(() => {  
        fetchSelectData();
        fetchInitialValues();
    }, [jwtData]);

    const fetchSelectData = async() => {
        if (!jwtData) return;

        const statusData = await statusService.getAll(jwtData);
        if (statusData) setData(statusData)
    };

    const fetchInitialValues = async() => {
        if (!id || !jwtData) return;

        let order = await orderService.getEntityEdit(id, jwtData);

        if (order) setInitialValues({
            id: id,
            statusId: order.statusId,
            comment: order.comment ?? ""
        })
    }

    const validate = (values: IOrderEditDTO): IOrderEditDTO => {
        const errors = {} as IOrderEditDTO;

        if (!values.statusId) {
            errors.statusId = "Required";
        }

        return errors;
    }

    const onSubmit = async (values: IOrderEditDTO) => {
        if (!jwtData || !id) return;

        let response = await orderService.edit(id, values, jwtData);
        if (response) navigate('../orders');
    }

    return (
        <OrderEditFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default OrderEdit;