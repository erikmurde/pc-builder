interface IProps {
    lg?: boolean
}

const PaymentItemStatus = (props: IProps) => {
    if (props.lg) {
        return (
            <div className="col-3 text-end d-none d-lg-block">
                Confirmed
                <i className="fas fa-circle ms-2 text-success"></i>
            </div>
        );
    }

    return (
        <div className="row text-start d-lg-none mb-1">
            <div className="col">
                <i className="fas fa-circle me-2 text-success"></i>
                Confirmed
            </div>
        </div>
    );
}

export default PaymentItemStatus;