import { useNavigate, useOutletContext } from "react-router-dom";
import ListCard from "../../../components/common/ListCard";
import Card from "../../../components/common/Card";
import { useState } from "react";
import { useAuth } from "../../auth/AuthContext";

export default function TransactionsIndex(){
    //Context
    const {token} = useAuth();
    const {accounts} = useOutletContext();

    //Routing
    const navigate = useNavigate();

    async function handleClick(event){
        const accountId = event.currentTarget.id;
        navigate(`/transactions/${accountId}`);
    }

    return (
        <div>
            {accounts.length > 0 ? <div className="container w-100 my-5 mx-4">
                <h2 className="mb-3 main-color"><b>Choose an Account</b></h2>
                <ul className="list-group mb-3">
                    {accounts.map(acc => {
                        return (
                            <div key={acc.id} style={{ cursor: "pointer" }}>
                                <ListCard 
                                id={acc.id} 
                                onClick={handleClick} 
                                title={<b className="fs-4">{acc.accountNumber}</b>} 
                                description={acc.createdAt} 
                                element={<b>{"$" + acc.balance}</b>}
                                class="mb-2 fs-5"/>
                            </div>
                        )
                    })}
                </ul>
            </div> : 
            <div>
                <h2 className="mb-3 main-color"><b>Choose an Account</b></h2>
                <b>There is No Accounts for this Profile!</b></div>}
        </div>

    )
}