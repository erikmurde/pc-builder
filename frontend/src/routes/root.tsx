import { Outlet } from "react-router-dom";
import { createContext, useState } from "react";
import Footer from "../components/Footer";
import Header from "../components/Header";
import { IJWTData } from "../dto/IJWTData";
import Interceptor from "../components/Interceptor";

interface IJwtContext {
    jwtData: IJWTData | null,
    setJwtData: (data: IJWTData | null) => void
}

interface ICartCountContext {
    cartCount: number,
    setCartCount: (count: number) => void
}

export const JwtContext = createContext<IJwtContext>({
    jwtData: null, 
    setJwtData: () => {}
});

export const CartCountContext = createContext<ICartCountContext>({
    cartCount: 0,
    setCartCount: () => {}
});

const Root = () => {
    const [jwtData, setJwtData] = useState(null as IJWTData | null);
    const [cartCount, setCartCount] = useState(0);

    return (
        <JwtContext.Provider value={{ jwtData, setJwtData }}>
            <CartCountContext.Provider value={{ cartCount, setCartCount }}>
                <Interceptor>
                    <Header/>
                    <div id="main-container">
                        <main role="main" className="mt-3 mb-3">
                            <div className="container shadow p-3" id="content-container">
                                <Outlet />
                            </div>
                        </main>
                    </div>
                    <Footer/>
                </Interceptor>
            </CartCountContext.Provider>
        </JwtContext.Provider>
    );
}

export default Root;