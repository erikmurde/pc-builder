import { useEffect, useState } from "react";
import { PackageSizeService } from "../../../services/packageSizeService";
import { Link, useParams } from "react-router-dom";
import DeleteModal from "../../../components/modal/DeleteModal";
import FormHeader from "../../../components/form/FormHeader";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";
import PackageSize from "../../../components/entity/packageSizes/PackageSize";

const PackageSizeDetails = (props: {onDelete: (id: string) => void}) => {
    const { id } = useParams();
    const service = new PackageSizeService();
    const [data, setData] = useState({} as IPackageSizeDTO);

    useEffect(() => {
        fetchPackageSize();
    }, [id]);

    const fetchPackageSize = async() => {
        if (!id) return;

        let packageSize = await service.getEntity(id);
        if (packageSize) setData(packageSize);
    }

    return (
        <div className="col content-panel content-scrollable">
            <FormHeader title="Package Size Details" nav="../packageSizes" btn="Back"/>
            <div className="row table-head">
                <div className="col-6">
                    Property
                </div>
                <div className="col-4">
                    Value
                </div>
                <div className="col-2">
                    <Link to={"../packageSizes/edit/" + data.id} className="fa-solid fa-pen-to-square text-success"></Link>
                    <DeleteModal id={data.id!} name="package size" nav="packageSizes" onDelete={props.onDelete}/>
                </div>
            </div>
            <PackageSize entity={data}/>
        </div>
    );
}

export default PackageSizeDetails;