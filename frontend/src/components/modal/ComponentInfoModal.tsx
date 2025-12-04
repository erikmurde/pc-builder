import { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import { Link } from 'react-router-dom';
import { ComponentService } from '../../services/componentService';
import { IComponentDetailsDTO } from '../../dto/component/IComponentDetailsDTO';
import ComponentAttributeListItem from '../entity/componentAttributes/ComponentAttributeListItem';
import EntityImageLarge from '../image/EntityImageLarge';

interface IProps {
    id: string
}

const ComponentInfoModal = (props: IProps) => {
    const service = new ComponentService();
    const [show, setShow] = useState(false);
    const [data, setData] = useState({} as IComponentDetailsDTO)
    const closeModal = () => setShow(false);

    const fetchComponent = async() => {
        let component = await service.getEntity(props.id);

        if (component) {
            setData(component);
            setShow(true); 
        }
    }

    let componentAttributes = [];
    if (data.componentAttributes) {
        for (let index = 0; index < data.componentAttributes.length; index++) {
            componentAttributes.push(<ComponentAttributeListItem 
                key={index} 
                index={index} 
                entity={data.componentAttributes[index]} 
                noLinks={true}/>);
        }
    }

    if (props.id === "None") {
        return(<div className="row"></div>);
    }

    return (
        <div className="row">
            <div className="col-12">
                <Link to="" role="button" 
                    className="text-center" 
                    onClick={() => {fetchComponent()}}>
                    <i className="fa-solid fa-circle-info text-primary m-0"></i>
                </Link>

                <Modal show={show} onHide={closeModal} centered 
                    dialogClassName="info-modal" 
                    contentClassName="info-modal shadow">
                    <Modal.Header closeButton className="border-primary">
                        <div className="col-11">
                            <Modal.Title>{data.componentName}</Modal.Title>
                        </div>
                    </Modal.Header>
                    <Modal.Body className="border-0">
                        <div className="row flex-center mb-2">
                            <EntityImageLarge src={data.imageSrc} alt={"Image of " + data.componentName} isNotRow={true}/>
                            <div className="col-7 col-sm-8">
                                {data.description}
                            </div>
                        </div>
                        {componentAttributes}
                    </Modal.Body>
                    <Modal.Footer className="border-0">
                    </Modal.Footer>
                </Modal>
            </div>
        </div>
    );
}

export default ComponentInfoModal