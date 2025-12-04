import NoItemsMessage from "../../../../components/profile/NoItemsMessage";
import ProfileHeader from "../../../../components/profile/ProfileHeader";

interface IProps {
    activeOrders: JSX.Element[],
    completedOrders: JSX.Element[],
    cancelledOrders: JSX.Element[]
}

const OrderHistoryView = (props: IProps) => {
    return (
        <> 
            <div className="row flex-center table-head m-0 p-2">
                <div className="col-12">
                    <h2>My Orders</h2>
                </div>
            </div>
            <ProfileHeader name="Active Orders"/>
            <NoItemsMessage list={props.activeOrders} message="There are currently no active orders."/>
            {props.activeOrders}
            <ProfileHeader name="Completed Orders"/>
            <NoItemsMessage list={props.completedOrders} message="You do not have any completed orders."/>
            {props.completedOrders}
            <ProfileHeader name="Cancelled Orders"/>
            <NoItemsMessage list={props.cancelledOrders} message="You have not cancelled any orders."/>
            {props.cancelledOrders}
        </>
    );
}

export default OrderHistoryView;