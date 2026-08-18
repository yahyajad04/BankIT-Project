import { useState } from "react"
import ListCard from "../../../components/common/ListCard"
import getAccounts from "../api/accountsApi";
import { useAuth } from "../../auth/AuthContext";
import { useLocation } from "react-router-dom";
import { useOutletContext } from "react-router-dom";

export default function AccountBalances(props){
    const {accounts, balance} = useOutletContext();
    return (
    <div className="container w-75">
    <h4 className="d-flex justify-content-between align-items-center mb-3">
    <span className="main-color">
      Balances
    </span>
    <span className="badge background-main rounded-pill">
      {accounts.length}
    </span>
  </h4>
  <ul className="list-group mb-3">
    <ListCard 
        title={<b className="fs-2">Total (USD)</b>}
        element={<b className="fs-2">{"$" + balance}</b>}/>

    {accounts.map(acc => {
        return(
            <ListCard 
                key={acc.id}
                title={acc.accountNumber}
                description={acc.createdAt}
                element={"$" + acc.balance}/>
        )
    })}
  </ul>
</div>
    )
}