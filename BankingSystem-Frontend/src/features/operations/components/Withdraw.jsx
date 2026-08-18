import { useOutletContext } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext"
import { useState } from "react";
import { getAccountByNumber , transfer, withdraw } from "../api/operations";
import { Input_col_sm_6 } from "../../../components/common/Input";
import SelectAccounts from "./SelectAccounts";
import { useNotification } from "../../../app/NotificationProvider";

export default function Withdraw(){
    const {token} = useAuth();
    const {accounts , refresh} = useOutletContext();
    const [fromAccount, setFromAccount] = useState("");
    const {showNotification} = useNotification();

    const [amount, setAmount] = useState(0);

    function handleChange(event){
        const {name, value} = event.target;

        if(name === "fromAccount")
            setFromAccount(value);
        else
            setAmount(Number(value));
    }


    async function handleClick(event){
        event.preventDefault();
        try{
            const withdrawDTO = {
                fromAccountId : fromAccount,
                amount: amount
            }
            console.log(withdrawDTO);
            const data = await withdraw(withdrawDTO, token);
            showNotification(
                "Withdraw Initiated Successfully" ,
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
                htmlFor="amount" 
                placeHolder="Enter the amount of money" 
                type="text" label="Amount" 
                id="Amount-id"
                handleChange={handleChange}
                value={amount}
                name="amount" />
            
            <button onClick={handleClick} type="submit" className="btn btn-dark mt-3" data-bs-dismiss="modal">Withdraw</button>
        </form>
    </div>
    )
}