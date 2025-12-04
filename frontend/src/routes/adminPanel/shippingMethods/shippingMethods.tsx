import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import ShippingMethodDetails from "./shippingMethodDetails";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import FormHeader from "../../../components/form/FormHeader";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";
import ShippingMethodListItem from "../../../components/entity/shippingMethods/ShippingMethodListItem";

const ShippingMethods = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new ShippingMethodService();
    const [data, setData] = useState([] as IShippingMethodDTO[]);

    useEffect(() => {  
        getAll(); 
    }, [jwtData]);

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await service.delete(id, jwtData);
        if (response) getAll();
    }

    const getAll = async () => {
        let response = await service.getAll();
        setData(response ? response : []);
    }

    if (id) {
        return (
            <ShippingMethodDetails onDelete={onDelete}/>
        );
    }

    let shippingMethods = [];
    for (let index = 0; index < data.length; index++) {
        shippingMethods.push(<ShippingMethodListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Shipping Methods" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-5">
                    Method Name
                </div>
                <div className="col-4 col-lg-5">
                    Shipping Time
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {shippingMethods}
        </div>
    );
}

export default ShippingMethods;