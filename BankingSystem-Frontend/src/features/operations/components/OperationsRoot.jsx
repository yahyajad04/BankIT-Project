import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext";
import getTransactionsAll from "../api/operations";
import getAccounts from "../../accounts/api/accountsApi";
import { useNotification } from "../../../app/NotificationProvider";

export default function OperationsRoot(){
    const {token} = useAuth();
    const [transactions, setTransactions] = useState([]);
    const [accounts, setAccounts] = useState([]);
    const [loading, setLoading] = useState(false);
    const {showNotification} = useNotification();

    async function refresh(){
        try{
            const data = await getTransactionsAll(token);
            setTransactions(data);
        }catch(err){
            showNotification(
                err.message,
                "danger"
            )
        }
    }
    useEffect(() => {
        async function fetchData(){
            try {
                setLoading(true);

                const [accountsData, transactionData] = await Promise.all([
                    getAccounts(token),
                    getTransactionsAll(token)
                ]);

                setAccounts(accountsData);
                setTransactions(transactionData);
            }catch(err){
                showNotification(
                    err.message,
                    "danger"
                )
            } 
            finally {
                setLoading(false);
            }
        }
        fetchData();
    },[]);

    return (
        <div className="container mx-5">
            <div>
                <Outlet context={
                    {
                    transactions,
                    accounts,
                    loading,
                    refresh
                    }
                }/>
            </div>
        </div>
    )
}