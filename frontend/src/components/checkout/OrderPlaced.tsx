import { Link } from "react-router-dom";

const OrderPlaced = () => {
    return (
        <div className="row flex-center">
            <div className="col-10 col-md-12 flex-center">
                <div className="card shadow text-center" id="order-placed">
                    <div className="mt-4 mb-3">
                        <i className="fa-regular fa-circle-check text-success"></i>
                    </div>
                    <div className="card-body d-flex flex-column align-items-center bg-light">
                        <h4 className="card-title mt-2">Thank you for your order!</h4>
                        <p className="card-text mb-0 mt-2">You can check the status of your order from your profile.</p>
                        <p className="card-text">You may also cancel the order at any time.</p>
                        <p className="text-muted">*All payments and orders are ficticious</p>
                        <Link 
                            to="../" 
                            role="button"
                            className="btn btn-success mt-auto rounded-0 shadow-sm">
                            <h5 className="mb-0">Return home</h5>
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default OrderPlaced;