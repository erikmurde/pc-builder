import { Field, Form, Formik } from "formik";
import FormTextInput from "../../../components/form/FormTextInput";

export interface ILoginFormData {
    email: string;
    password: string;
}

const LoginFromView = (props: {
    initialValues: ILoginFormData,
    validate: (values: ILoginFormData) => ILoginFormData,
    onSubmit: (values: ILoginFormData, setStatus: (status: any) => void) => void
    }) => {

    return (
        <div className="row flex-center">
            <div className="col-6 content-panel">
                <div className="row content-head mb-4">
                    <h2 className="text-center">Login</h2>
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
                        <div className={"row text-center mb-3" + (formik.status === "Invalid" ? " d-flex" : " d-none")}>
                            <span className="text-danger">Invalid username or password</span>
                        </div>
                        <div className="row justify-content-center mb-3">
                            <div className="col-8 text-center">
                                <button type="submit" className="w-50 btn btn-lg btn-primary">Login</button>
                            </div>
                        </div>     
                    </Form>)}
                </Formik>
            </div>
        </div>
    );
}

export default LoginFromView;