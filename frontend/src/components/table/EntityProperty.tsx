const EntityProperty = (props: {name?: string, value: string | number, isEven: boolean}) => {
    if (!props.name) {
        return(
            <div className="row entity-row">
                <div className="col-12">
                    {props.value}
                </div>
            </div>
        );
    }

    return (
        <div className={"row entity-row " + (props.isEven ? "row-even" : "")}>
            <div className="col-6">
                {props.name}
            </div>
            <div className="col-6">
                {props.value}
            </div>
        </div>
    );
}

export default EntityProperty;