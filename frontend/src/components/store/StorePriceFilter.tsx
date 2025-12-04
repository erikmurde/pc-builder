import { useState } from "react";
import { Link } from "react-router-dom";

interface IProps {
    maxPrice: number,

    setMaxPrice: (value: number) => void
}

const StorePriceFilter = (props: IProps) => {
    const [show, setShow] = useState(true);

    return (
        <>
            <div className="row flex-center filter-row">
                <div className="col-10">
                    <Link
                        to=""
                        role="button"
                        className="text-decoration-none link-dark"
                        onClick={() => setShow(!show)}>
                        <h4>Price</h4>
                    </Link>
                </div>
                <div className="col-2 text-center">
                    <i className={"fas " + (show ? "fa-angle-up" : "fa-angle-down")}></i>
                </div>
            </div>
            <hr className="mt-0 mb-2"/>
            <div className={"row filter-row ps-4 pe-4 mb-2 " + (show ? "" : "d-none")}>
                <label htmlFor="priceRange">${props.maxPrice} or less</label>
                <input 
                    id="priceRange" 
                    type="range" 
                    min="500"
                    max="5000" 
                    step="100" 
                    defaultValue="5000" 
                    className="p-0"
                    onChange={(e) => props.setMaxPrice(Number(e.target.value))}/>
            </div>
        </>
    );
}

export default StorePriceFilter;