import { Field, Form, Formik } from "formik";
import { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { JwtContext } from "../root";
import { IUserReviewCreateDTO } from "../../dto/userReview/IUserReviewCreateDTO";
import { UserReviewService } from "../../services/userReviewService";
import FormHeader from "../../components/form/FormHeader";
import TableHead from "../../components/table/TableHead";
import FormSelectInput from "../../components/form/FormSelectInput";
import FormTextAreaInput from "../../components/form/FormTextAreaInput";
import { PcBuildService } from "../../services/pcBuildService";
import { IPcBuildDetailsDTO } from "../../dto/pcBuild/IPcBuildDetailsDTO";

const ReviewCreate = () => {
    const { jwtData } = useContext(JwtContext);
    const { id } = useParams();
    const navigate = useNavigate();
    const userReviewService = new UserReviewService();
    const pcBuildService = new PcBuildService();

    const [data, setData] = useState({} as IPcBuildDetailsDTO);
    const initialValues = {
        rating: "",
        reviewContent: "",
        pcBuildId: id!
    }

    useEffect(() => {
        if (!jwtData || !id) return navigate("../login");

        fetchPcBuild();
    }, []);

    const fetchPcBuild = async() => {
        let response = await pcBuildService.getEntity(id!, jwtData!);
        if (response) setData(response);
    }

    const validate = (values: IUserReviewCreateDTO) => {
        const errors = {} as IUserReviewCreateDTO;

        if (!values.rating) {
            errors.rating = "Required";
        }
        if (!values.reviewContent) {
            errors.reviewContent = "Required";
        } else if (values.reviewContent.length > 2048) {
            errors.reviewContent = "Must be 2048 characters or less";
        }

        return errors;
    }

    const onSubmit = async (values: IUserReviewCreateDTO) => {
        if (!jwtData) return;

        let response = await userReviewService.create(values, jwtData);
        if (response) navigate(-1);
    }

    let ratingSelectValues = [];
    for (let index = 0; index < 5; index++) {
        ratingSelectValues.push({name: index + 1, value: index + 1});    
    }

    return (
        <div className="row flex-center">
            <div className="col-8 content-panel content-scrollable">
                <FormHeader title="Write a Review" nav={"../prebuilt-pcs/" + id} btn="Back"/>
                <Formik
                    initialValues={initialValues}
                    validate={(values) => validate(values)}
                    onSubmit={(values) => onSubmit(values)}>
                    <Form>
                        <TableHead title={"Reviewing " + (data ? data.pcName : "")} btnName="Submit"/>
                        <div className="row mt-3">
                            <Field name="rating" label="Rating" selectValues={ratingSelectValues} lengthLg={3} component={FormSelectInput}/>
                        </div>
                        <div className="row">
                            <Field name="reviewContent" label="Review Content" component={FormTextAreaInput}/>
                        </div>
                    </Form>
                </Formik>
            </div>
        </div>
    );
}

export default ReviewCreate;