import { Outlet } from "react-router-dom";
import AccountsHeader from "./AccountsHeader";
import { useAuth } from "../../auth/AuthContext";
import { useState , useEffect } from "react";
import getAccounts from "../api/accountsApi";

export default function AccountsRoot(){
    const {token} = useAuth();
    const [accounts, setAccounts] = useState([]);
    const [balance, setBalance] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchAccounts();
    }, []);
    
    useEffect(() => {
        const total = accounts.reduce(
            (sum, acc) => sum + acc.balance,
            0
        );

        setBalance(total);
    }, [accounts]);
    
    async function fetchAccounts() {
        try {
            setLoading(true)
            const data = await getAccounts(token);
            setAccounts(data);
        } catch (err) {
            console.error(err);
        }finally{
            setLoading(false)
        }
    }
    return (
        <div className="container mx-5">
            <AccountsHeader />
            <div className="">
                <Outlet context={
                    {accounts,
                    setAccounts,
                    fetchAccounts,
                    balance,
                    loading}
                } />
            </div>
        </div>
    )
}