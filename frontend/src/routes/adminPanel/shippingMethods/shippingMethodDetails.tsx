import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import FormHeader from "../../../components/form/FormHeader";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";
import ShippingMethod from "../../../components/entity/shippingMethods/ShippingMethod";

const ShippingMethodDetails = (props: {onDelete: (id: string) => void}) => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new ShippingMethodService();
    const [data, setData] = useState({} as IShippingMethodDTO);

    useEffect(() => {
        fetchShippingMethod();
    }, [jwtData]);
    
    const fetchShippingMethod = async() => {
        if (!id) return;

        let shippingMethod = await service.getEntity(id);
        if (shippingMethod) setData(shippingMethod);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Shipping Method Details" nav="../shippingMethods" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../shippingMethods/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="shipping method" nav="shippingMethods" onDelete={props.onDelete}/>
                </div>
            </div>
            <ShippingMethod entity={data}/>
        </div>
    );
}

export default ShippingMethodDetails;