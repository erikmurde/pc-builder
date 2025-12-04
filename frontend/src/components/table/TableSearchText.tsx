interface IProps {
    label: string,
    inputType?: string,
    filterType?: number

    setFilter: (value: string, type?: number) => void
}

const TableSearchText = (props: IProps) => {
    return (
        <div className="form-floating">
            <input 
                type={props.inputType ?? "text"}
                className="form-control" 
                id="nameFilter" 
                placeholder="#"
                onChange={
                    (e) => props.setFilter(e.target.value, props.filterType)
                }/>
            <label htmlFor="nameFilter">{props.label}</label>
        </div>
    );
}

export default TableSearchText;