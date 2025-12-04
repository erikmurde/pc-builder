import { useContext, useEffect, useState } from "react";
import { CartPcService } from "../../services/cartPcService";
import { ShippingCostService } from "../../services/shippingCostService";
import { CartCountContext, JwtContext } from "../root";
import CheckoutView, { ICheckoutFormValues } from "./CheckoutView";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import { useNavigate } from "react-router-dom";
import { IdentityService } from "../../services/identityService";
import { ShippingMethodService } from "../../services/shippingMethodService";
import { IShippingMethodDTO } from "../../dto/shippingMethod/IShippingMethodDTO";
import { IShippingCostDTO } from "../../dto/shippingCost/IShippingCostDTO";
import { StatusService } from "../../services/statusService";
import { DiscountService } from "../../services/discountService";
import { PackageSizeService } from "../../services/packageSizeService";
import { OrderService } from "../../services/orderService";
import OrderPlaced from "../../components/checkout/OrderPlaced";
import InternalError from "../../components/InternalError";
import { IPackageSizeDTO } from "../../dto/packageSize/IPackageSizeDTO";
import { ComponentService } from "../../services/componentService";
import { IComponentDetailsDTO } from "../../dto/component/IComponentDetailsDTO";

const Checkout = () => {
    const cartPcService = new CartPcService();
    const shippingMethodService = new ShippingMethodService();
    const shippingCostService = new ShippingCostService();
    const orderService = new OrderService();
    const statusService = new StatusService();
    const discountService = new DiscountService();
    const packageSizeService = new PackageSizeService();
    const identityService = new IdentityService();
    const componentService = new ComponentService();
    const navigate = useNavigate();
    const { jwtData } = useContext(JwtContext);
    const { setCartCount } = useContext(CartCountContext);

    const [cartData, setCartData] = useState([] as ICartPcDTO[]);
    const [pcCaseData, setPcCaseData] = useState([] as IComponentDetailsDTO[])
    const [selectData, setSelectData] = useState({
        shippingMethods: [] as IShippingMethodDTO[],
        shippingCosts: [] as IShippingCostDTO[],
        packageSizes: [] as IPackageSizeDTO[]
    });

    const [success, setSuccess] = useState(false);

    const initialValues = {
        shippingMethodId: "",
        firstName: "",
        lastName: "",
        email: jwtData ? identityService.getJwtObject(jwtData).email : "",
        phoneNumber: "",
        streetAddress: "",
        city: "",
        zipCode: "",
        comment: "",
    };

    useEffect(() => { 
        if (!jwtData) return navigate("../login");
        fetchCartPcs();
        fetchSelectValues();
    }, []);

    useEffect(() => {
        fetchPcCases();
    }, [cartData])

    const fetchPcCases = () => {
        cartData.forEach(async cartPc => {
            let caseId = cartPc.pcBuild.pcComponents
                .filter(c => c.categoryName === "Case")[0].componentId;

            let pcCase = await componentService.getEntity(caseId);
            if (pcCase) setPcCaseData(caseData => [...caseData, pcCase!]);
        });
    }
    
    const fetchCartPcs = async() => {
        let response = await cartPcService.getAll(jwtData!);

        if (!response) return <InternalError message={"Failed to load checkout page"}/>
        else if (response.length === 0) {
            return navigate("../cart");
        }

        setCartData(response);
    }

    const fetchSelectValues = async() => {
        let shippingMethodData = await shippingMethodService.getAll();
        let shippingCostData = await shippingCostService.getAll(jwtData!);
        let packageSizeData = await packageSizeService.getAll();

        if (!shippingCostData || !shippingMethodData || !packageSizeData) {
            return <InternalError message={"Failed to load checkout page"}/>;
        }

        setSelectData({
            shippingMethods: shippingMethodData, 
            shippingCosts: shippingCostData,
            packageSizes: packageSizeData
        });
    }

    const validate = (values: ICheckoutFormValues) => {
        const errors = {} as ICheckoutFormValues;

        if (!values.firstName) {
            errors.firstName = "Required";
        } else if (values.firstName.length > 128) {
            errors.firstName = "Must be 128 characters or less"
        }
        if (!values.lastName) {
            errors.lastName = "Required";
        } else if (values.lastName.length > 128) {
            errors.lastName = "Must be 128 characters or less"
        }
        if (!values.email) {
            errors.email = "Required"
        } else if (values.email.length > 128) {
            errors.email = "Must be 128 characters or less"
        } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
            errors.email = "Invalid email address"
        }
        if (!values.phoneNumber) {
            errors.phoneNumber = "Required"
        } else if (!/^\d+$/.test(values.phoneNumber)) {
            errors.phoneNumber = "Invalid value"
        } else if (values.phoneNumber.length > 32) {
            errors.phoneNumber = "Must be 32 characters or less"
        }
        if (!values.streetAddress) {
            errors.streetAddress = "Required"
        } else if (values.streetAddress.length > 128) {
            errors.streetAddress = "Must be 128 characters or less"
        }
        if (!values.city) {
            errors.city = "Required"
        } else if (values.city.length > 128) {
            errors.city = "Must be 128 characters or less"
        }
        if (!values.zipCode) {
            errors.zipCode = "Required"
        } else if (!/^\d+$/.test(values.zipCode)) {
            errors.zipCode = "Invalid value"
        } else if (values.zipCode.length > 32) {
            errors.zipCode = "Must be 32 characters or less"
        }
        if (!values.shippingMethodId) {
            errors.shippingMethodId = "Required";
        }

        return errors;
    }

    const onSubmit = async(values: ICheckoutFormValues, setStatus: (status: any) => void) => {
        if (!jwtData) return navigate("../login");

        let statusData = await statusService.getAll();
        let discountData = await discountService.getAll();

        if (!statusData || !discountData) return;

        // Check pcs for stock. Useful for edge cases as stock errors should be caught in cart
        if (!cartData.every(c => cartPcService.checkStock(c))) {
            setStatus("Invalid");
            return;
        }

        let order = orderService.createOrder(values, {
            cartPcs: cartData,
            statuses: statusData,
            packageSizes: selectData.packageSizes,
            discounts: discountData,
            shippingMethods: selectData.shippingMethods,
            shippingCosts: selectData.shippingCosts,
            pcCases: pcCaseData
        });
        
        console.log(order);

        let response = await orderService.create(order, jwtData);

        if (response) {
            // Empty cart
            cartData.forEach(cartPc => {
                cartPcService.delete(cartPc.id, jwtData);
            });
            if (setCartCount) setCartCount(0);
            setSuccess(true);
        }
    }

    if (success) {
        return <OrderPlaced />
    }
    return (
        <CheckoutView cartPcs={cartData} initialValues={initialValues} selectValues={selectData} validate={validate} onSubmit={onSubmit}/>
    );
}

export default Checkout;