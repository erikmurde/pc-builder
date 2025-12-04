import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { ShippingCostService } from "../../../services/shippingCostService";
import { IShippingCostDTO } from "../../../dto/shippingCost/IShippingCostDTO";
import ShippingCost from "../../../components/entity/shippingCosts/ShippingCost";

const ShippingCostDetails = (props: {onDelete: (id: string) => void}) => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new ShippingCostService();
    const [data, setData] = useState({} as IShippingCostDTO);

    useEffect(() => {
        fetchShippingCost();
    }, [jwtData]);

    const fetchShippingCost = async() => {
        if (!id || !jwtData) return;

        let shippingCost = await service.getEntity(id, jwtData);
        if (shippingCost) setData(shippingCost);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Shipping Cost Details" nav="../shippingCosts" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../shippingCosts/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="shipping cost" nav="shippingCosts" onDelete={props.onDelete}/>
                </div>
            </div>
            <ShippingCost entity={data}/>
        </div>
    );
}

export default ShippingCostDetails;