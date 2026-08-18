import { Outlet } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext";
import { useEffect, useState } from "react";
import getAccounts from "../../accounts/api/accountsApi";
import getLoans , {getLoanTypes} from "../api/loanApi";
import LoansHeader from "./LoansHeader";
import { useNotification } from "../../../app/NotificationProvider";

export default function LoanRoot(){
    const {token} = useAuth();
    const [accounts, setAccounts] = useState([]);
    const [loans, setLoans] = useState([]);
    const [types, setTypes] = useState([]);
    const [loading, setLoading] = useState(false);
    const {showNotification} = useNotification();

    async function refresh(){
        try{
            const data = await getLoans(token);
            setLoans(data);
        }catch(err){
            showNotification(
                err.message,
                "danger"
            )
        }
    }

    async function fetchData(){
        try{
            setLoading(true);

            const [accountsDTO, LoansDTO, typesDTO] = await Promise.all([
                getAccounts(token),
                getLoans(token),
                getLoanTypes()
            ]);

            setAccounts(accountsDTO);
            setLoans(LoansDTO);
            setTypes(typesDTO);
        }catch(err){
            showNotification(
                err.message,
                "danger"
            )
        }finally{
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchData()
    }, []);

    return(
        <div className="container mx-5">
            <LoansHeader />
            <div>
                <Outlet context={
                    {
                        accounts,
                        loans,
                        refresh,
                        loading,
                        types
                    }
                }/>
            </div>
        </div>
    )
}