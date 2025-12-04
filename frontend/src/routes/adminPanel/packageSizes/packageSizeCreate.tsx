import { useContext } from "react";
import PackageSizeCreateFormView from "./views/packageSizeCreateFormView";
import { useNavigate } from "react-router-dom";
import { JwtContext } from "../../root";
import { PackageSizeService } from "../../../services/packageSizeService";
import { IPackageSizeCreateDTO } from "../../../dto/packageSize/IPackageSizeCreateDTO";

const PackageSizeCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const navigate = useNavigate();
    const service = new PackageSizeService();

    const initialValues = {
        sizeName: ''
    }

    const validate = (values: IPackageSizeCreateDTO) => {
        const errors = {} as IPackageSizeCreateDTO;

        if (!values.sizeName) {
            errors.sizeName = 'Required';
        } else if (values.sizeName.length > 64) {
            errors.sizeName = 'Must be 64 characters or less';
        }
        
        return errors;
    }

    const onSubmit = async (values: IPackageSizeCreateDTO) => {
        if (!jwtData) return;

        let response = await service.create(values, jwtData);
        if (response) navigate('../packageSizes');
    }

    return (
        <PackageSizeCreateFormView initialValues={initialValues} validate={validate} onSubmit={onSubmit} />
    );
}

export default PackageSizeCreate;