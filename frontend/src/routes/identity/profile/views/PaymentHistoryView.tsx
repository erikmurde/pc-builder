import NoItemsMessage from "../../../../components/profile/NoItemsMessage";

interface IProps {
    payments: JSX.Element[]
}

const PaymentHistoryView = (props: IProps) => {
    return (
        <> 
            <div className="row flex-center table-head m-0 mb-3 p-2">
                <div className="col-12">
                    <h2>My Payments</h2>
                </div>
            </div>
            <NoItemsMessage list={props.payments} message="You have not made any payments."/>
            {props.payments}
        </>
    );
}

export default PaymentHistoryView;