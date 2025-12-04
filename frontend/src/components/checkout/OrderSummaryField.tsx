interface IProps {
    name: string,
    value: number,
    strong?: boolean
}

const OrderSummaryField = (props: IProps) => {
    let name = props.strong 
    ? <h5>{props.name}</h5> 
    : <span>{props.name}</span>
  
    let value = props.strong 
    ? <h5>${props.value}</h5> 
    : <span>{props.value > 0 ? "$" + props.value : "No method chosen"}</span>

    return (
        <div className="row text-start ps-2 pe-2 p-1">
            <div className="col-4">
                {name}
            </div>
            <div className="col-8 text-end">
                {value}
            </div>
        </div>
    );
}

export default OrderSummaryField;