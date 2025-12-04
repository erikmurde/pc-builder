import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import { DiscountService } from "../../../services/discountService";
import Discount from "../../../components/entity/discounts/Discount";
import FormHeader from "../../../components/form/FormHeader";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";

const DiscountDetails = (props: {onDelete: (id: string) => void}) => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new DiscountService();
    const [data, setData] = useState({} as IDiscountDTO);

    useEffect(() => {
        fetchDiscount();
    }, [jwtData]);
    
    const fetchDiscount = async() => {
        if (!id) return;

        let discount = await service.getEntity(id);
        if (discount) setData(discount);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Discount Details" nav="../discounts" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../discounts/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="discount" nav="discounts" onDelete={props.onDelete}/>
                </div>
            </div>
            <Discount entity={data}/>
        </div>
    );
}

export default DiscountDetails;