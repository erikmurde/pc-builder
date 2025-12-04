import { useState } from "react";
import StoreFilter from "./StoreFilter";
import { Link } from "react-router-dom";
import { IFilter } from "../../routes/store/StorePage";

interface IProps {
    title: string,
    type: number,
    filters: IFilter[],

    updateFilters: (checked: boolean, filter: string, type: number) => void
}

const StoreFilterGroup = (props: IProps) => {
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
                        <h4>{props.title}</h4>
                    </Link>
                </div>
                <div className="col-2 text-center">
                    <i className={"fas " + (show ? "fa-angle-up" : "fa-angle-down")}></i>   
                </div>
            </div>
            <hr className="mt-0 mb-2"/>
            <ul className={show ? "ps-1" : "d-none"}>
                {props.filters.map(filter =>
                <StoreFilter key={filter.value} filter={filter} type={props.type} updateFilters={props.updateFilters}/>)}
            </ul>
        </>
    );
}

export default StoreFilterGroup;