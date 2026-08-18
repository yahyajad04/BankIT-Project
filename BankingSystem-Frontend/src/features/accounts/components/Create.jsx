import Input,{Input_4col, Input_col_sm_6, Input_textArea} from "../../../components/common/Input"
import Select from "../../../components/common/Select";
import getAccountTypes from "../api/accountTypesApi";
import { useAuth } from "../../auth/AuthContext";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { createAccount } from "../api/accountsApi";
import { useNotification } from "../../../app/NotificationProvider";

export default function Create(props){
    //Context
    const {token} = useAuth();
    const navigate = useNavigate();  
    const {showNotification} = useNotification();

    //State
    const [types, setTypes] = useState([]);
    const [type, setType] = useState(-1);
    const [balance, setBalance] = useState(0);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [image, setImage] = useState("");


        useEffect(() => {
            async function fetchAccountTypes() {
                try {
                    const data = await getAccountTypes(token);
                    setTypes(data);
                } catch (err) {
                    showNotification(
                        err.message,
                        "danger"
                    );
                }
            }
            fetchAccountTypes();
        }, []);

        function handleChange(event){
            var { value, name } = event.target;

            if(name === "balance")
                setBalance(Number(value));
            else if(name === "type")
                setType(Number(value));
            else if(name === "accountName")
                setName(value)
            else 
                setDescription(value);
        }

        async function handleClick(event){
            event.preventDefault();
            event.currentTarget.blur();

            if(type != -1){
                const createAccountDTO = {
                    AccountTypeId : type,
                    Balance: balance,
                    AccountName: name,
                    AccountDescription: description
                }
                try{
                    var data = await createAccount(token, createAccountDTO);
                    showNotification(
                        "Account Created Successfully",
                        "success"
                    )
                    await props.refresh();
                }catch(err){
                    showNotification(
                        err.message,
                        "danger"
                    )
                }
            }else                    
                showNotification(
                        "Choose a Type please",
                        "danger"
                    )
        }

    return(
        <div>
            <form method="POST">
                <Select 
                    values={types} 
                    title="Select Account Type" 
                    handleChange={handleChange}
                    value={type}
                    name="type"/>
                
                <div className="row">
                    <Input_col_sm_6 
                        htmlFor="balance" 
                        placeHolder="Enter the amount of money" 
                        type="text" label="Balance" 
                        id="balance-input-id"
                        handleChange={handleChange}
                        value={balance}
                        name="balance" />
                    <Input_col_sm_6 
                        htmlFor="Name" 
                        placeHolder="ex.Main Account" 
                        type="text" label="Account Name (optional)" 
                        id="Account-Name-id"
                        handleChange={handleChange}
                        value={name}
                        name="accountName" />
                </div>

                <div className="row">
                    <Input_textArea 
                        htmlFor="description" 
                        placeHolder="ex.This is for savings" 
                        type="text" title="Description (optional)" 
                        id="Account-Description-id"
                        handleChange={handleChange}
                        value={description}
                        name="accountDescription"
                        class="mt-3" />
                </div>

                
                <button onClick={handleClick} type="submit" className="btn btn-dark mt-3" data-bs-dismiss="modal">Create Account</button>
            </form>
        </div>
    )
}