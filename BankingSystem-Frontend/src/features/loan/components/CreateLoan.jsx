import SelectAccounts from "../../operations/components/SelectAccounts";
import Input, {Input_col_sm_6, Input_4col} from "../../../components/common/Input"
import { useAuth } from "../../auth/AuthContext"
import { useOutletContext } from "react-router-dom";
import { useState } from "react";
import { createLoan } from "../api/loanApi";
import SelectLoanType from "./SelectLoanType";
import { useNotification } from "../../../app/NotificationProvider";

export default function CreateLoan(props){
    const {token} = useAuth();
    const {accounts , types , refresh} = useOutletContext();
    const {showNotification} = useNotification();
    
    const [account, setAccount] = useState(-1);
    const [amount, setAmount] = useState(0);
    const [type, setType] = useState("");
    const [monthsToPay, setMonthsToPay] = useState(0);


    function handleChange(event){
        const {name, value} = event.target;

        if(name === "account")
            setAccount(value)
        else if(name === "amount")
            setAmount(value)
        else if(name === "type")
            setType(value);
        else
            setMonthsToPay(value);
    }

    async function handleClick(event){
        event.preventDefault();

        const loanDTO = {
            AccountId : account,
            Amount : amount,
            Type : type,
            MonthsRepay : monthsToPay
        }

        try{
            const data = await createLoan(loanDTO , token);
            showNotification(
                "Loan Created Successfully",
                "success"
            )
            refresh();
        }catch(err){
            showNotification(
                err.message,
                "danger"
            )
        }
    }
    return (
         <div>
            <form method="POST">
                <SelectAccounts 
                    values={accounts} 
                    title="Select Account" 
                    handleChange={handleChange}
                    value={account}
                    name="account"/>

                <div className="mb-3"/>
                
                <Input_col_sm_6 
                htmlFor="amount" 
                placeHolder="Enter the amount of money" 
                type="text" label="Loan Amount" 
                id="Loan-amount-id"
                handleChange={handleChange}
                value={amount}
                name="amount" />

                <div className="mb-3"/>

                <div className="row">
                    <div className="col-6">
                        <SelectLoanType 
                            values={types} 
                            title="Select Loan Type" 
                            handleChange={handleChange}
                            value={type}
                            name="type"/>
                    </div>
                    <Input_col_sm_6 
                        htmlFor="months" 
                        placeHolder="Enter Number of Months to Pay the Loan" 
                        type="text" label="Months To Pay" 
                        id="loan-months-id"
                        handleChange={handleChange}
                        value={monthsToPay}
                        name="months" />
                </div>
                
                <button onClick={handleClick} type="submit" className="btn btn-dark mt-3" data-bs-dismiss="modal">Create Loan</button>
            </form>
        </div>
    )
}