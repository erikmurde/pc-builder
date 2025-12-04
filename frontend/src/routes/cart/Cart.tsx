import { useContext, useEffect, useState } from "react";
import { CartCountContext, JwtContext } from "../root";
import { CartPcService } from "../../services/cartPcService";
import { ICartPcDTO } from "../../dto/cartPc/ICartPcDTO";
import { useNavigate } from "react-router-dom";
import CartItem from "../../components/shoppingCart/CartItem";
import { PcBuildService } from "../../services/pcBuildService";
import CartView from "./CartView";
import EmptyCartView from "./EmptyCartView";
import { ICartPcEditDTO } from "../../dto/cartPc/ICartPcEditDTO";

const Cart = () => {
    const { jwtData } = useContext(JwtContext);
    const { cartCount, setCartCount } = useContext(CartCountContext);
    const cartPcService = new CartPcService();
    const pcBuildService = new PcBuildService();
    const navigate = useNavigate();
    const [data, setData] = useState([] as ICartPcDTO[]);

    useEffect(() => { 
        if (!jwtData) return navigate("../login");
        fetchAll();
    }, []);

    const fetchAll = async() => {
        let response = await cartPcService.getAll(jwtData!);
        setData(response ? response : []);
    }

    const onDelete = async(id: string) => {
        if (!jwtData) return;

        let pcBuild = data.filter(c => c.id == id)[0].pcBuild;

        let cartPcResponse = await cartPcService.delete(id, jwtData!);
        if (cartPcResponse) {
            if (setCartCount) setCartCount(cartCount - 1);

            // Do not delete the PC build if it is a prebuilt
            if (!pcBuild.isCustom) {
                fetchAll();
                return;
            }

            // Delete custom PC build
            let pcBuildResponse = await pcBuildService.delete(pcBuild.id, jwtData);
            if(pcBuildResponse) fetchAll();
        }
    }

    const onEdit = async(id: string, pcBuildId: string) => {
        if (!jwtData) return;

        let cartPcResponse = await cartPcService.delete(id, jwtData);
        if (cartPcResponse) {
            if (setCartCount) setCartCount(cartCount - 1);
            navigate("../configurator/" + pcBuildId);
        }
    }

    const onSubmit = async(values: ICartPcEditDTO) => {
        let current = data.filter(c => c.id == values.id)[0];
        
        if (current.qty === Number(values.qty)) return;

        let response = await cartPcService.edit(values.id, values, jwtData!);
        if (response) fetchAll();
    }

    const validate = (values: ICartPcEditDTO) => {
        const errors = {} as ICartPcEditDTO;

        var cartPc = data.filter(c => c.id === values.id)[0];

        if (values.qty === "") {
            errors.qty = "Required";
        } else if (!/^\d+$/.test(values.qty)) {
            errors.qty = "Invalid value";
        } else if (Number(values.qty) <= 0) {
            errors.qty = "Too small"
        } else if (Number(values.qty) > 10) {
            errors.qty = "Too large";
        } else if (!cartPcService.checkStock({
            id: cartPc.id, 
            pcBuild: cartPc.pcBuild, 
            qty: Number(values.qty)})) {
                
            errors.qty = "Insufficient stock";
        }

        return errors;
    }

    let cartItems: JSX.Element[] = [];
    data.forEach(cartItem => {
        cartItems.push(<CartItem key={cartItem.id} entity={cartItem} 
            onDelete={onDelete} 
            onEdit={onEdit}
            onSubmit={onSubmit}
            validate={validate}/>)
    });

    if (data.length === 0) {
        return (
            <EmptyCartView />
        );
    }

    return (
        <CartView data={data} cartItems={cartItems}/>
    );
}

export default Cart;