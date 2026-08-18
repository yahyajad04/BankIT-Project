import { useOutletContext } from "react-router-dom";
import Select from "../../../components/common/Select";
import { useState } from "react";
import NativeSwitch from "../../../components/common/RadioButtons";
import { MdOutlineMenu , MdAccountBalance } from "react-icons/md";

export default function AccountSort(){
    const {accounts, setAccounts} = useOutletContext();
    const [choice , setChoice] = useState("");
    const [order , setOrder] = useState(true);
    const features = ["date" , "accountNumber" , "balance"];

    function handleChange(event){
        const {name, value , checked} = event.target;

        if(name === "choice")
            setChoice(value);
        else if(name === "order")
            setOrder(checked);
    }

    function handleClick(){
        var sortAccounts = [...accounts];
        if(choice === "accountNumber"){
            sortAccounts = sortAccounts.sort((a, b) =>
               order ? a.accountNumber - b.accountNumber 
                : b.accountNumber - a.accountNumber 
                );   
        }
        else if(choice === "balance"){
            sortAccounts = sortAccounts.sort((a, b) =>
                order ? a.balance - b.balance
                : b.balance - a.balance
                );       
        }
        else if(choice === "date"){
            sortAccounts = sortAccounts.sort((a, b) =>
                order ? a.createdAt.localeCompare(b.createdAt)
                : b.createdAt.localeCompare(a.createdAt)
                );          
        }
        setAccounts(sortAccounts);
    }

    return (
                <div className="row">
                    <div className="col-3 d-flex align-items-center">
                        <MdAccountBalance className="me-2" size={42}/>
                        <b className="fs-3 main-color">Your Accounts</b>
                    </div>
                    
                    <div className="col-3 ms-auto">
                        <Select 
                        handleChange={handleChange}
                        value={choice}
                        name="choice"
                        title="Sort Option"
                        values={features}/>
                    </div>
                    <div className="col-1 d-flex justify-content-center align-items-center">
                        <NativeSwitch 
                        handleChange={handleChange}
                        checked={order}
                        name="order"
                        label="Asc" />
                    </div>
                    <div className="col-1">
                        <button onClick={handleClick} className="btn btn-dark d-inline-flex align-items-center" type="button">
                            Sort
                        </button>
                    </div>
                </div>

    )
}