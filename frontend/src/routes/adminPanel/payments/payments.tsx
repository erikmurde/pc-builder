import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { PaymentService } from "../../../services/paymentService";
import { IPaymentDTO } from "../../../dto/payment/IPaymentDTO";
import PaymentDetails from "./paymentDetails";
import PaymentListItem from "../../../components/entity/payments/PaymentListItem";
import TableSearchText from "../../../components/table/TableSearchText";

const Payments = () => {
    const service = new PaymentService();
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();

    const [data, setData] = useState([] as IPaymentDTO[]);
    const [filter, setFilter] = useState("");

    useEffect(() => {  
        getAll(); 
    }, [jwtData]);

    const getAll = async () => {
        if (!jwtData) return;

        let response = await service.getAll(jwtData);
        setData(response ? response.sort((a, b) => a.paymentDate > b.paymentDate ? -1 : 1) : []);
    }
    
    const filterData = (data: IPaymentDTO[]) => {
        return data.filter(p => 
            p.userEmail.toUpperCase().includes(filter.toUpperCase())
        );
    }

    if (id) {
        return (
            <PaymentDetails/>
        );
    }

    let filteredData = filterData(data);

    let payments = [];
    for (let index = 0; index < filteredData.length; index++) {
        payments.push(<PaymentListItem key={index} index={index} entity={filteredData[index]}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Payments" nav="create"/>
            <div className="row mt-2">
                <div className="col-6 col-lg-4">
                    <TableSearchText label="Username" filterType={0} setFilter={(value: string) => setFilter(value)}/>
                </div>
            </div>
            <div className="row table-head">
                <div className="col-3 col-lg-2">
                    Payment Nr
                </div>
                <div className="col-4 col-lg-2">
                    Paid By
                </div>
                <div className="col-4 d-none d-lg-block">
                    Payment Date
                </div>
                <div className="col-2 d-none d-lg-block">
                    Amount Paid
                </div>
                <div className="col-3 col-lg-2 d-lg-none">
                    Amount
                </div>
                <div className="col-2">
                    Actions
                </div>
            </div>
            {payments}
        </div>
    );
}

export default Payments;