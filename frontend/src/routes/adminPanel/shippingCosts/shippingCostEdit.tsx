import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import ShippingCostEditFormView from "./views/shippingCostEditFormView";
import { IShippingCostEditDTO } from "../../../dto/shippingCost/IShippingCostEditDTO";
import { ShippingCostService } from "../../../services/shippingCostService";
import { PackageSizeService } from "../../../services/packageSizeService";
import { ShippingMethodService } from "../../../services/shippingMethodService";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import { IShippingMethodDTO } from "../../../dto/shippingMethod/IShippingMethodDTO";

const ShippingCostEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const shippingCostService = new ShippingCostService();
    const packageSizeService = new PackageSizeService();
    const shippingMethodService = new ShippingMethodService();

    const [selectData, setData] = useState({
        packageSizes: [] as IPackageSizeDTO[],
        shippingMethods: [] as IShippingMethodDTO[]
    });

    const [initialValues, setInitialValues] = useState({
        id: "",
        packageSizeId: "",
        shippingMethodId: "",
        costPerUnit: "" 
    });

    useEffect(() => {
        fetchSelectData();
        fetchInitialValues();
    }, [jwtData]);

    const fetchSelectData = async() => {
        const packageSizeData = await packageSizeService.getAll();
        const shippingMethodData = await shippingMethodService.getAll();

        if (packageSizeData && shippingMethodData) {
            setData({packageSizes: packageSizeData, shippingMethods: shippingMethodData})
        }
    };

    const fetchInitialValues = async() => {
        if (!id || !jwtData) return;

        let shippingCost = await shippingCostService.getEntityEdit(id, jwtData);

        if (shippingCost) setInitialValues({
            id: id,
            packageSizeId: shippingCost.packageSizeId,
            shippingMethodId: shippingCost.shippingMethodId,
            costPerUnit: shippingCost.costPerUnit
        })
    }


    const validate = (values: IShippingCostEditDTO) => {
        const errors = {} as IShippingCostEditDTO;

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

    const onSubmit = async (values: IShippingCostEditDTO) => {
        if (!jwtData || !id) return;

        let response = await shippingCostService.edit(id, values, jwtData);
        if (response) navigate('../shippingCosts');
    }

    return (
        <ShippingCostEditFormView initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit} />
    );
}

export default ShippingCostEdit;