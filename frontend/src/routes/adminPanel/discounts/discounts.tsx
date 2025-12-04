import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { useParams } from "react-router-dom";
import DiscountListItem from "../../../components/entity/discounts/DiscountListItem";
import DiscountDetails from "./discountDetails";
import { DiscountService } from "../../../services/discountService";
import FormHeader from "../../../components/form/FormHeader";
import { IDiscountDTO } from "../../../dto/discount/IDiscountDTO";

const Discounts = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new DiscountService();
    const [data, setData] = useState([] as IDiscountDTO[]);

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
            <DiscountDetails onDelete={onDelete}/>
        );
    }

    let discounts = [];
    for (let index = 0; index < data.length; index++) {
        discounts.push(<DiscountListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Discounts" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-5">
                    Discount Name
                </div>
                <div className="col-4 col-lg-5">
                    Discount
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {discounts}
        </div>
    );
}

export default Discounts;