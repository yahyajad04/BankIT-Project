import { useAuth } from "../../auth/AuthContext";
import { useEffect, useState } from "react";
import getAccounts from "../api/accountsApi";
import AccountsCard from "../components/AccountsCard";
import InfoModal from "../components/InfoModal";
import AccountsHeader from "../components/AccountsHeader";
import Info from "../components/Info";
import Create from "../components/Create";
import AccountBalances from "./AccountsBalances";
import { useOutletContext } from "react-router-dom";
import AccountSort from "../components/AccountSort";
import Spinner from "../../../components/common/Spinner";

export default function Accounts(){

const { accounts, fetchAccounts , loading} = useOutletContext();
    return (
        <div className="container mx-5">
            {loading ? <Spinner /> : (accounts.length !== 0)  ? <div>
                <AccountSort />
                <div className="row mb-5">
                    {accounts.map(acc => {
                    return (
                        <div key={acc.id} className="col-sm-12 col-md-6 col-lg-4 col-xl-4 mt-4">
                            <AccountsCard account={acc} />
                            <InfoModal id={`account-${acc.id}`} element={<Info account={acc}/>}/>
                        </div>
                    )
                    })}
                    <div className="col-4">
                        <InfoModal id="create-account-id" element={<Create refresh={fetchAccounts} />}/>
                    </div>
                    <div className="col-4">
                        <InfoModal id="accounts-balance-id" element={<AccountBalances />}/>
                    </div>               
                </div>
            </div> : 
                <div>
                    <h2 className="main-color"><b>Your Accounts</b></h2>
                    <b>There is no Accounts for this Profile!</b>
                    <div className="col-4">
                        <InfoModal id="create-account-id" element={<Create refresh={fetchAccounts} />}/>
                    </div>
                </div>}
        </div>  
    )
}