import { useOutletContext } from "react-router-dom";
import OperationsButtons from "../components/OperationsButtons";
import Modal from "../../../components/common/Modal";
import Transfer from "../components/Transfer";
import { getAccount } from "../../accounts/api/accountsApi";
import { useAuth } from "../../auth/AuthContext";
import Deposit from "../components/Deposit";
import Withdraw from "../components/Withdraw";
import Spinner from "../../../components/common/Spinner";
import TransactionsList from "../components/TransactionsList";

export default function OperationsIndex(){
    const {transactions, loading, accounts} = useOutletContext();

    return (
    <div className="container mx-3 my-4">
        <OperationsButtons />

        <div className="col-4">
            <Modal id="transfer-id" element={<Transfer />}/>
        </div>  

        <div className="col-4">
            <Modal id="deposit-id" element={<Deposit />}/>
        </div>  
        
        <div className="col-4">
            <Modal id="withdraw-id" element={<Withdraw />}/>
        </div> 
        
        {transactions.length != 0 ? <TransactionsList transactions={transactions} accounts={accounts} /> : loading ? 
            <Spinner />
        : <div><b>There is no transactions for this Profile!</b></div>}
    </div>
    );
}