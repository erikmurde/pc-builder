import { Link } from "react-router-dom";
import FormHeader from "../../components/form/FormHeader";

const EmptyCartView = () => {
    return (
        <div className="row justify-content-center">
            <div className="col-md-9 content-panel text-center shadow border-0">
                <FormHeader title="SHOPPING CART"/>
                <div className="row justify-content-center">
                    <div className="col-12 mb-5">
                        <img className="mt-4" src="https://content.ibuypower.com/Images/Carts/noItem.png" alt="Drawing of a shopping cart"></img>
                        <h3 className="mt-2">There are no items in the cart.</h3>
                        Try adding some products to the shopping cart.
                    </div>
                    <div className="col-4 col-lg-3 mb-5">
                        <Link to="../" className="btn btn-primary">Return home</Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EmptyCartView;