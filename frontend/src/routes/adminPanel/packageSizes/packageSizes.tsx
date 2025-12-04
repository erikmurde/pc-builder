import { useContext, useEffect, useState } from "react";
import { JwtContext } from "../../root";
import { PackageSizeService } from "../../../services/packageSizeService";
import { useParams } from "react-router-dom";
import PackageSizeDetails from "./packageSizeDetails";
import FormHeader from "../../../components/form/FormHeader";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import PackageSizeListItem from "../../../components/entity/packageSizes/PackageSizeListItem";

const PackageSizes = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const service = new PackageSizeService();
    const [data, setData] = useState([] as IPackageSizeDTO[]);

    useEffect(() => {  
        getAll(); 
    }, [jwtData]);

    const onDelete = async (id: string) => {
        if (!id || !jwtData) return;

        let response = await service.delete(id, jwtData);
        if (response) getAll();
    }

    const getAll = async () => {
        let response = await service.getAll();
        setData(response ? response : []);
    }

    if (id) {
        return (
            <PackageSizeDetails onDelete={onDelete}/>
        );
    }

    let packageSizes = [];
    for (let index = 0; index < data.length; index++) {
        packageSizes.push(<PackageSizeListItem key={index} index={index} entity={data[index]} onDelete={onDelete}/>);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Manage Package Sizes" nav="create" btn="New"/>
            <div className="row table-head">
                <div className="col-9 col-lg-10">
                    Size Name
                </div>
                <div className="col-3 col-lg-2">
                    Actions
                </div>
            </div>
            {packageSizes}
        </div>
    );
}

export default PackageSizes;