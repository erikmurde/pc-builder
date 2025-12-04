import { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { Link, useNavigate } from 'react-router-dom';

interface IModalProps {
    id: string,
    name: string,
    nav: string,
    linkName?: string

    onDelete: (id: string) => void;
}

const DeleteModal = (props: IModalProps) => {
    const [show, setShow] = useState(false);
    const navigate = useNavigate();
    const closeModal = () => setShow(false);

    return (
        <>
            <Link 
                to="." 
                className={props.linkName ? "text-decoration-none text-danger" : "fa-solid fa-trash text-danger"} 
                role="btn" 
                onClick={() => setShow(true)}>
                {props.linkName}
            </Link>

            <Modal show={show} onHide={closeModal} centered>
            <Modal.Header closeButton className="border-0">
                <Modal.Title>Confirm delete</Modal.Title>
            </Modal.Header>
            <Modal.Body className="border-0">Are you sure you want to delete this {props.name}?</Modal.Body>
            <Modal.Footer className="border-0">
                <button className="btn btn-secondary" onClick={closeModal}>
                    Cancel
                </button>
                <button className="btn btn-danger" onClick={() => {
                    props.onDelete(props.id); 
                    closeModal(); 
                    navigate(props.nav ? "../" + props.nav : ".");
                    }}>
                <i className="fa-solid fa-trash text-white"></i>
                    Delete
                </button>
            </Modal.Footer>
            </Modal>
        </>
    );
}

export default DeleteModal