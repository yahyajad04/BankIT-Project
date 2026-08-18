import ListCard from "../../../components/common/ListCard";
import Modal from "../../../components/common/Modal";
import TransactionsInfo from "./TransactionsInfo";

export default function TransactionsListCard(props){
    const transactions = props.transactions;
    const account = props.account;
    return(
        <ul className="list-group mb-3">
            {transactions.map(transaction => {
                const fromAccount = transaction.fromAccount;
                const toAccount = transaction.toAccount;                
                const x =  (transaction.type === "Transfer" && account.id === fromAccount.id) ? false : true;
                return (
                    <div key={transaction.id} style={{ cursor: "pointer" }}>
                        <ListCard 
                            id={transaction.id} 
                            title={<b className="fs-4">{transaction.type} </b>} 
                            description={transaction.createdAt} 
                            element={<b className="fs-4">{"$" + transaction.amount}</b>}
                            dataToggle="modal"
                            dataTarget={`#transaction-id-${transaction.id}`}
                            class={(transaction.type === "Deposit" || transaction.type ===  "Loan" || x) ? "bg-success bg-opacity-25"
                                : (transaction.type === "Withdraw" || !x) ? "bg-danger-subtle" 
                                :  ""}/>
                        
                        <Modal 
                                id={`transaction-id-${transaction.id}`} 
                                title="Transaction Info" 
                                element={<TransactionsInfo 
                                            transaction={transaction} 
                                            fromAccount={fromAccount}
                                            toAccount={toAccount} />} />
                    </div>
                )
            })}
        </ul>
    )
}