import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../../root";
import { PackageSizeService } from "../../../services/packageSizeService";
import PackageSizeEditFormView from "./views/packageSizeEditFormView";
import { IPackageSizeDTO } from "../../../dto/packageSize/IPackageSizeDTO";

const PackageSizeEdit = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const service = new PackageSizeService();
    const [initialValues, setInitialValues] = useState({id: "", sizeName: ""});

    useEffect(() => {  
        fetchInitialValues();
    }, [jwtData]);

    const fetchInitialValues = async() => {
        if (!id) return;

        let packageSize = await service.getEntity(id);

        if (packageSize) setInitialValues({
            id: id,
            sizeName: packageSize.sizeName
        })
    }

    const validate = (values: IPackageSizeDTO): IPackageSizeDTO => {
        const errors = {} as IPackageSizeDTO;

        if (!values.sizeName) {
            errors.sizeName = 'Required';
        } else if (values.sizeName.length > 64) {
            errors.sizeName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: IPackageSizeDTO) => {
        if (!jwtData || !id) return;

        let response = await service.edit(id, values, jwtData);
        if (response) navigate('../packageSizes');
    }

    return (
        <PackageSizeEditFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default PackageSizeEdit;