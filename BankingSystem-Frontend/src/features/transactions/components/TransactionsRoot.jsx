import { useAuth } from "../../auth/AuthContext";
import { useState , useEffect } from "react";
import { Outlet } from "react-router-dom";
import getAccounts from "../../accounts/api/accountsApi";
import TransactionsHeader from "./TransactionsHeader";
import { useNotification } from "../../../app/NotificationProvider";

export default function TransactionsRoot(){
        const {token} = useAuth();
        const [accounts, setAccounts] = useState([]);
        const {showNotification} = useNotification();
        
        useEffect(() => {
            fetchAccounts();
        }, []);
        
        async function fetchAccounts() {
            try {
                const data = await getAccounts(token);
                setAccounts(data);
            } catch (err) {
                showNotification(
                    err.message,
                    "danger"
                )
            }
        }
    return (
         <div className="container mx-5">
            <div className="">
                <Outlet context={
                    {accounts,
                    setAccounts,
                    fetchAccounts,}
                } />
            </div>
        </div>
    )
}