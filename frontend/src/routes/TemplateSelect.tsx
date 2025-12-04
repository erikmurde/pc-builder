import { useEffect, useState } from "react";
import { IPcBuildDTO } from "../dto/pcBuild/IPcBuildDTO";
import { PcBuildService } from "../services/pcBuildService";
import TemplatePcCard from "../components/card/TemplatePcCard";

const PcTemplates = () => {
    const service = new PcBuildService();
    const [data, setData] = useState([] as IPcBuildDTO[]);

    useEffect(() => {  
        getAllTemplates(); 
    }, []);

    const getAllTemplates = async () => {
        let response = await service.getAll();
        setData(response ? response
            .filter(p => p.categoryName === "Template PC")
            .sort((a, b) => (a.cost > b.cost) ? 1 : -1) : []);
    }

    return (
        <>
            <div className="row flex-center">
                <h3 className="col text-center">Intel Templates</h3>
            </div>
            <hr className="config-hr mb-3"/>
            <div className="row justify-content-center mb-3">
                {data.filter(p => p.pcName.includes("Intel"))
                .map(pcBuild =>
                    <TemplatePcCard key={pcBuild.id} entity={pcBuild}/>
                )}
            </div>
            <div className="row flex-center">
                <h3 className="col text-center">AMD Templates</h3>
            </div>
            <hr className="config-hr mb-3"/>
            <div className="row justify-content-center">
                {data.filter(p => p.pcName.includes("AMD"))
                .map(pcBuild =>
                    <TemplatePcCard key={pcBuild.id} entity={pcBuild}/>
                )}
            </div>
        </>
    );
}

export default PcTemplates;