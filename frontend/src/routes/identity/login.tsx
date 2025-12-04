import { useContext } from "react";
import { IdentityService } from "../../services/identityService";
import { CartCountContext, JwtContext } from "../root";
import LoginFromView, { ILoginFormData } from "./views/loginFormView";
import { useNavigate } from "react-router-dom";
import { CartPcService } from "../../services/cartPcService";

const Login = () => {
    const identityService = new IdentityService();
    const cartPcService = new CartPcService();
    const navigate = useNavigate();
    const { setJwtData } = useContext(JwtContext);
    const { setCartCount } = useContext(CartCountContext);

    const initialValues = {
        email: '',
        password: ''
    }
    
    const validate = (values: ILoginFormData): ILoginFormData => {
        const errors = {} as ILoginFormData;

        if (!values.email) {
            errors.email = 'Required'
        } else if (values.email.length > 128) {
            errors.email = 'Must be 128 characters or less'
        } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(values.email)) {
            errors.email = 'Invalid email address'
        }

        if (!values.password) {
            errors.password = 'Required';
        } else if (values.password.length < 6) {
            errors.password = 'Must be at least 6 characters'
        } else if (values.password.length > 64) {
            errors.password = 'Must be 64 characters or less'
        } else if (!/^(?=.*[0-9])(?=.*[!@#$%^&*.])[a-zA-Z0-9!@#$%^&*.]{6,16}$/.test(values.password)) {
            errors.password = 'Password too weak'
        }

        return errors;
    }

    const onSubmit = async (values: ILoginFormData, setStatus: (status: any) => void) => {
        let jwtResponse = await identityService.login(values);

        if (!identityService.isJwtData(jwtResponse)) {
            setStatus("Invalid");
            return;
        }

        let cartPcResponse = await cartPcService.getAll(jwtResponse);
        if (cartPcResponse) setCartCount(cartPcResponse.length);

        setJwtData(jwtResponse);
        navigate("../home");
    }

    return (
        <LoginFromView initialValues={initialValues} validate={validate} onSubmit={onSubmit}/>
    );
}

export default Login;