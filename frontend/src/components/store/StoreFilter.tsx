import { IFilter } from "../../routes/store/StorePage";

interface IProps {
    filter: IFilter,
    type: number

    updateFilters: (checked: boolean, filter: string, type: number) => void
}

const StoreFilter = (props: IProps) => {
    return (
        <li className="mt-1 d-inline-block d-lg-block">
            <div className="form-check ms-2">
                <label className="form-check-label">
                    <input
                    className="form-check-input border-primary rounded-0"
                    type="checkbox"
                    onChange={(e) => props.updateFilters(e.target.checked, props.filter.value, props.type)}
                    />
                    {props.filter.label ?? props.filter.value}
                </label>
            </div>
        </li>
    );
}

export default StoreFilter;