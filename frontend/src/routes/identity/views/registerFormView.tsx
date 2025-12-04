import FormTextInput from "../../../components/form/FormTextInput";
import { Field, Form, Formik } from "formik";

export interface IRegisterFormData {
    email: string;
    password: string;
    confirmPassword: string;
}

const RegisterFromView = (props: {
    initialValues: IRegisterFormData,
    validate: (values: IRegisterFormData) => IRegisterFormData,
    onSubmit: (values: IRegisterFormData, setStatus: (status: any) => void) => void
    }) => {

    return (
        <div className="row flex-center">
            <div className="col-6 content-panel">
                <div className="row content-head mb-4">
                    <h2 className="text-center">Create a new account</h2>
                </div>
                <Formik
                    initialValues={props.initialValues}
                    validate={(values) => props.validate(values)}
                    onSubmit={(values, { setStatus }) => props.onSubmit(values, setStatus)}>
                    {(formik => <Form>         
                        <div className="row justify-content-center">
                            <Field type="email" name="email" label="Email" length={9} lengthLg={7} component={FormTextInput}/>
                        </div>
                        <div className="row justify-content-center">
                            <Field type="password" name="password" label="Password" length={9} lengthLg={7} component={FormTextInput}/>
                        </div>
                        <div className="row justify-content-center">
                            <Field type="password" name="confirmPassword" label="Confirm Password" length={9} lengthLg={7} component={FormTextInput}/>
                        </div>
                        <div className={"row text-center mb-3" + (formik.status === "Taken" ? " d-flex" : " d-none")}>
                            <span className="text-danger">Email is already taken</span>
                        </div>
                        <div className="row justify-content-center mb-3">
                            <div className="col-8 text-center">
                                <button type="submit" className="w-50 btn btn-lg btn-primary">Register</button>    
                            </div>
                        </div>
                    </Form>)}
                </Formik>
            </div>
        </div>
    );
}

export default RegisterFromView;