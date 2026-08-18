import { useParams , useNavigate, useOutletContext } from "react-router-dom";
import { useState , useEffect } from "react";
import { useAuth } from "../../auth/AuthContext";
import PriceCard from "../../../components/common/PriceCard";
import { useTransactions } from "../hooks/useTransactions";
import Spinner from "../../../components/common/Spinner";
import TransactionsListCard from "../components/TransactionsListCards";

export default function Transactions(){
    //Routing
    const navigate = useNavigate();
    const { accountId } = useParams();

    //Context
    const {token} = useAuth();
    const {accounts} = useOutletContext();

    //State
    const {
        loading,
        account,
        transactions
    } = useTransactions(token, accountId);

    return (
        <div className="container mx-3 my-4">
            <div className="col-3 mb-5 mx-auto">
                {account && <PriceCard 
                title={<b className="fs-3">{"Account: " + account.accountNumber}</b>}
                main={"Balance:     $" + account.balance}/>}
            </div>

            <div className="col-4 mb-5">
            </div>
            {transactions.length != 0 ? <TransactionsListCard transactions={transactions} account={account} />: loading ? 
               <Spinner />
            : <div><b>There is no transactions for this account!</b></div>}
        </div>
    )
}