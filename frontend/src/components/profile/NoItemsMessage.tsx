interface IProps {
    list: any[],
    message: string
}

const NoItemsMessage = (props: IProps) => {
    return (
        <div className={"row m-0 p-2 mb-2" + (props.list.length > 0 ? " d-none" : "")}>
            {props.message}
        </div>
    );
}

export default NoItemsMessage;