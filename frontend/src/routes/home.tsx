import { useState, useEffect } from "react";
import { IPcBuildDTO } from "../dto/pcBuild/IPcBuildDTO";
import { PcBuildService } from "../services/pcBuildService";
import { IPcBuildStoreDTO } from "../dto/pcBuild/IPcBuildStoreDTO";
import TemplatePcCard from "../components/card/TemplatePcCard";
import { Link } from "react-router-dom";
import PrebuiltPcCard from "../components/card/PrebuiltPcCard";

const Home = () => {
    const service = new PcBuildService();
    const [templatePcs, setTemplatePcs] = useState([] as IPcBuildDTO[]);
    const [preebuiltPcs, setPrebuiltPcs] = useState([] as IPcBuildStoreDTO[]);
    
    useEffect(() => {  
        getAllTemplates();
        getAllPrebuilts(); 
    }, []);

    const getAllTemplates = async () => {
        let response = await service.getAll();
        setTemplatePcs(response ? response
            .filter(p => p.categoryName === "Template PC")
            .sort((a, b) => a.cost > b.cost ? 1 : -1)
            .slice(0, 3) : []);
    }

    const getAllPrebuilts = async () => {
        let response = await service.getAllStore();
        setPrebuiltPcs(response ? response
            .filter(p => p.discountPercentage > 0)
            .slice(0, 3) : []);
    }

    return (
        <>
            <div className="container">
                <h3 className="text-center">Welcome to PC Builder.</h3>
                <h4 className="text-center">Choose a system or configure your own PC</h4>
                <hr className="config-hr mt-2 mb-3"/>
                <div className="row flex-center mb-2 p-0 m-0">
                    <div className="col-12 col-xxl-2 text-center mb-2">
                        <h3 className="mb-0">Template PCs</h3>
                        <Link 
                            className="text-decoration-none home-link" 
                            to="../templates">
                            View more
                        </Link>
                    </div>
                    {templatePcs.map(pcBuild =>
                        <TemplatePcCard key={pcBuild.id} entity={pcBuild}/>
                    )}
                </div>
                <hr className="config-hr mb-3"/>
                <div className="row flex-center mb-3 p-0 m-0">
                    <div className="col-12 col-xxl-2 text-center mb-2">
                        <h3 className="mb-0">Prebuilt Systems</h3>
                        <Link 
                            className="text-decoration-none home-link" 
                            to="../prebuilt-pcs">
                            View more
                        </Link>
                    </div>
                    {preebuiltPcs.map(pcBuild =>
                        <PrebuiltPcCard key={pcBuild.id} entity={pcBuild} nav={`../prebuilt-pcs/${pcBuild.id}`}/>
                    )}
                </div>
            </div>
        </>
    );
}

export default Home;