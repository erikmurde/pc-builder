interface IProps {
    selectValues: {name: string, value: string}[],
    label: string,
    type?: number

    setFilter: (value: string, type?: number) => void
}

const TableSearchSelect = (props: IProps) => {
    return (
        <div className="form-floating">
            <select 
                className="form-select" 
                id={props.label}
                placeholder="#"
                onChange={
                    (e) => props.setFilter(e.target.value, props.type)
                }>
                <option value="" defaultChecked>All</option>
                {props.selectValues.map((c, index) =>
                    <option key={index} value={c.value}>{c.name}</option>    
                )}
            </select>
            <label htmlFor={props.label}>{props.label}</label>
        </div>
    );
}

export default TableSearchSelect;