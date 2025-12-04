import { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { Link } from 'react-router-dom';
import { IOrderEditDTO } from '../../dto/order/IOrderEditDTO';

interface IModalProps {
    id: string,
    statusId: string,

    onSubmit: (values: IOrderEditDTO) => void
}

const OrderCancelModal = (props: IModalProps) => {
    const [show, setShow] = useState(false);
    const closeModal = () => setShow(false);

    return (
        <>
            <Link to="" className="text-decoration-none link-danger" role="btn" onClick={() => setShow(true)}>cancel order</Link>

            <Modal show={show} onHide={closeModal} centered>
            <Modal.Header closeButton className="border-0">
                <Modal.Title>Confirm cancellation</Modal.Title>
            </Modal.Header>
            <Modal.Body className="border-0">Are you sure you wish to cancel this order?</Modal.Body>
            <Modal.Footer className="border-0">
                <button className="btn btn-secondary w-25 shadow-sm" onClick={closeModal}>
                    No
                </button>
                <button className="btn btn-danger w-25 shadow-sm" onClick={() => {
                    props.onSubmit({
                        id: props.id,
                        statusId: props.statusId,
                        comment: ""
                    }); 
                    closeModal();
                }}>
                    Yes
                </button>
            </Modal.Footer>
            </Modal>
        </>
    );
}

export default OrderCancelModal;