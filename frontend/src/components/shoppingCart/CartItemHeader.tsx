import { Field, FormikProps } from "formik";
import { ICartPcEditDTO } from "../../dto/cartPc/ICartPcEditDTO";

interface IProps {
    form: FormikProps<ICartPcEditDTO>,
    pcName: string,
    costPerUnit: number
}

const CartItemHeader = (props: IProps) => {
    let error = props.form.errors["qty"]?.toString();

    return (
        <div className="row flex-center cart-item-header p-2 mt-2">
            <div className="col-4 text-start">
                <h5>{props.pcName}</h5>
            </div>
            <div className="col-4 col-lg-5 col-xl-6 text-end p-0">
                <span className="text-danger">{error}&nbsp;</span>
                <Field 
                    name="qty" 
                    className={error ? "qty-field-invalid" : "qty-field"} 
                    id="qty" 
                    placeholder="Qty">
                </Field>
            </div>
            <div className="col-4 col-lg-3 col-xl-2 text-end">
                <strong>${props.costPerUnit}</strong> / unit
            </div>
            <hr className="cart-hr"/>
        </div>
    )
}

export default CartItemHeader;