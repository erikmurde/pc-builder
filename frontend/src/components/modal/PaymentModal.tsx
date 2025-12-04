import { useContext, useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { Link } from 'react-router-dom';
import { IPaymentEditDTO } from '../../dto/payment/IPaymentEditDTO';
import { Field, Form, Formik } from 'formik';
import FormTextAreaInput from '../form/FormTextAreaInput';
import { PaymentService } from '../../services/paymentService';
import { JwtContext } from '../../routes/root';

interface IModalProps {
    id: string,

    onSubmit: (values: IPaymentEditDTO) => void
}

const PaymentModal = (props: IModalProps) => {
    const service = new PaymentService();
    const { jwtData } = useContext(JwtContext); 
    const [show, setShow] = useState(false);
    const [comment, setComment] = useState("");
    const closeModal = () => setShow(false);

    const getComment = async(id: string) => {
        if (!jwtData) return;

        let response = await service.getEntityEdit(id, jwtData);
        if (response) setComment(response.comment);
    }

    return (
        <Formik
            initialValues={{id: props.id, comment: comment}}
            onSubmit={(values: IPaymentEditDTO) => props.onSubmit(values)}
            enableReinitialize>
            <Form id={props.id}>
                <Link to="" className="text-decoration-none link-success" role="btn" onClick={() => {
                    getComment(props.id);
                    setShow(true);
                }}>
                    edit notes
                </Link>

                <Modal show={show} onHide={closeModal} centered>
                <Modal.Header closeButton className="border-0">
                    <Modal.Title>Write notes about your payment</Modal.Title>
                </Modal.Header>
                <Modal.Body className="border-0">
                    <Field name="comment" label="Notes" length={12} component={FormTextAreaInput}/>
                </Modal.Body>
                <Modal.Footer className="border-0">
                    <button type="button" className="btn btn-secondary w-25 shadow-sm" onClick={closeModal}>
                        Cancel
                    </button>
                    <button type="submit" className="btn btn-success w-25 shadow-sm" form={props.id} onClick={closeModal}>
                        Confirm
                    </button>
                </Modal.Footer>
                </Modal>
            </Form>
        </Formik>
    );
}

export default PaymentModal;