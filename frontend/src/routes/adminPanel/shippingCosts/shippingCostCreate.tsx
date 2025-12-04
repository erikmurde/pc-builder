import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import ShippingCostCreateFormView from "./views/shippingCostCreateFormView";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";
import { ShippingCostService } from "../../../services/shippingCostService";
import { PackageSizeService } from "../../../services/packageSizeService";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import { IShippingCostCreateDTO } from "../../../dto/shippingCost/IShippingCostCreateDTO";

const ShippingCostCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const shippingCostService = new ShippingCostService();
    const packageSizeService = new PackageSizeService();
    const shippingMethodService = new ShippingMethodService();

    const [selectData, setData] = useState({
        packageSizes: [] as IPackageSizeDTO[],
        shippingMethods: [] as IShippingMethodDTO[],
    });

    useEffect(() => {
        fetchSelectData();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const packageSizeData = await packageSizeService.getAll();
        const shippingMethodData = await shippingMethodService.getAll();

        if (packageSizeData && shippingMethodData) {
            setData({packageSizes: packageSizeData, shippingMethods: shippingMethodData})
        }
    };

    const initialValues = {
        packageSizeId: "",
        shippingMethodId: "",
        costPerUnit: ""
    }

    const validate = (values: IShippingCostCreateDTO) => {
        const errors = {} as IShippingCostCreateDTO;

        if (!values.packageSizeId) {
            errors.packageSizeId = "Required";
        }
        if (!values.shippingMethodId) {
            errors.shippingMethodId = "Required";
        }
        if (!values.costPerUnit) {
            errors.costPerUnit = "Required"
        } else if (parseInt(values.costPerUnit) < 0 || parseInt(values.costPerUnit) >= 100000) {
            errors.costPerUnit = "Must be between 0 and 99,999.99";
        } else if (!/^\d+(\.\d{1,2})?$/.test(values.costPerUnit)) {
            errors.costPerUnit = "Must have up to 2 decimal places";
        }

        return errors;
    }

    const onSubmit = async (values: IShippingCostCreateDTO) => {
        if (!jwtData) return;

        let response = await shippingCostService.create(values, jwtData);
        if (response) navigate('../shippingCosts');
    }

    return (
        <ShippingCostCreateFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ShippingCostCreate;