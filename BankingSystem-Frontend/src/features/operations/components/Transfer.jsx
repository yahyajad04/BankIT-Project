import { useOutletContext } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext"
import { useState } from "react";
import { getAccountByNumber , transfer } from "../api/operations";
import { Input_col_sm_6 } from "../../../components/common/Input";
import SelectAccounts from "./SelectAccounts";
import { useNotification } from "../../../app/NotificationProvider";

export default function Transfer(){
    const {token} = useAuth();
    const {accounts , refresh} = useOutletContext();
    const [fromAccount, setFromAccount] = useState("");
    const [toAccountId, setToAccountId] = useState(0);
    const {showNotification} = useNotification();
    const [amount, setAmount] = useState(0);

    function handleChange(event){
        const {name, value} = event.target;

        if(name === "fromAccount")
            setFromAccount(value);
        else if(name === "amount")
            setAmount(Number(value));
        else
            setToAccountId(Number(value));
    }

    async function checkAccount(){
        try{
            const data = await getAccountByNumber(toAccountId, token);
            return data;
        }catch(err){
            throw new Error("This Account Doesnt Exist");
        }
    }

    async function handleClick(event){
        event.preventDefault();
        try{
            const toAccount = await checkAccount();

            const transferDTO = {
                fromAccountId : fromAccount,
                toAccountId : toAccount.id,
                amount: amount
            }
            const data = await transfer(transferDTO, token);
            showNotification(
                "Transfer Initiated Successfully",
                "success"
            )
            await refresh();
        }catch(err){
            showNotification(
                err.message ?? err ,
                "danger"
            )
        }
    }
    return(
    <div>
        <form method="POST">

            <SelectAccounts 
                values={accounts} 
                title="Select Account" 
                handleChange={handleChange}
                value={fromAccount}
                name="fromAccount"/>

            <hr className="mb-3"/>
            <Input_col_sm_6 
                htmlFor="to Account Number" 
                placeHolder="Enter the Account Number or Id" 
                type="text" label="To Account Number" 
                id="AccountNumber-id"
                handleChange={handleChange}
                value={toAccountId}
                name="AccountNumber" />

            <Input_col_sm_6 
                htmlFor="amount" 
                placeHolder="Enter the amount of money" 
                type="text" label="Amount" 
                id="Amount-id"
                handleChange={handleChange}
                value={amount}
                name="amount" />
            
            <button onClick={handleClick} type="submit" className="btn btn-dark mt-3" data-bs-dismiss="modal">Transfer</button>
        </form>
    </div>
    )
}