interface IProps {
    name: string
}

const ProfileHeader = (props: IProps) => {
    return (
        <>
            <div className="row m-0 mt-3 p-2">
                <h4>{props.name}</h4>
            </div>
            <hr className="config-hr mb-2"/>
        </>
    );
}

export default ProfileHeader;