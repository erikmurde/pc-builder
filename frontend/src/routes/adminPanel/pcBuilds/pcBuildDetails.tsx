import { useEffect, useState } from "react";
import { PcBuildService } from "../../../services/pcBuildService";
import { Link, useParams } from "react-router-dom";
import FormHeader from "../../../components/form/FormHeader";
import { IPcBuildDetailsDTO } from "../../../dto/pcBuild/IPcBuildDetailsDTO";
import DeleteModal from "../../../components/modal/DeleteModal";
import PcBuild from "../../../components/entity/pcBuilds/PcBuild";
import PcComponentListItem from "../../../components/entity/pcComponents/PcComponentListItem";

const PcBuildDetails = (props: {onDelete: (id: string) => void}) => {
    const { id } = useParams();
    const service = new PcBuildService();
    const [data, setData] = useState({} as IPcBuildDetailsDTO);

    useEffect(() => {
        fetchPcBuild();
    }, [id]);
    
    const fetchPcBuild = async() => {
        if (!id) return;

        let pcBuild = await service.getEntity(id);
        if (pcBuild) setData(pcBuild);
    }

    let pcComponents = [];
    if (data.pcComponents) {
        for (let index = 0; index < data.pcComponents.length; index++) {
            pcComponents.push(<PcComponentListItem key={index} index={index} entity={data.pcComponents[index]}/>);
        }
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="PC Build Details" nav="../pcBuilds" btn="Back"/>
            <div className="row table-head">
                <div className="col-10">
                    Overall Details
                </div>
                <div className="col-2 text-center">
                    <Link to={"../pcBuilds/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="PC build" nav="pcBuilds" onDelete={props.onDelete}/>
                </div>
            </div>
            <PcBuild entity={data}/>
            <div className="row table-head">
                <div className="col-7">
                    Component Name
                </div>
                <div className="col-3">
                    Category
                </div>
                <div className="col-2">
                    Price
                </div>
            </div>
            {pcComponents}
        </div>
    );
}

export default PcBuildDetails;