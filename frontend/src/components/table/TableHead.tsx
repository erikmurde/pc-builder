const TableHead = (props: {title: string, btnName?: string}) => {
    if (props.btnName) {
        return (
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    {props.title}
                </div>
                <div className="col-3 col-lg-2 p-0">
                    <button type="submit" className="btn btn-success btn-crud">
                        <i className={"fa-solid text-white " + (props.btnName === "Create" ? "fa-file-circle-plus" : "fa-pen-to-square")}></i>
                        {props.btnName}
                    </button>
                </div>
            </div>
        );
    }

    return (
        <div className="row table-head">
            <div className="col-12">
                {props.title}
            </div>
        </div>
    );
}

export default TableHead;