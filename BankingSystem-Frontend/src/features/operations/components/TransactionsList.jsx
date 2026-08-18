import ListCard from "../../../components/common/ListCard";

export default function TransactionsList(props){
    const transactions = props.transactions;
    const accounts = props.accounts;
    return (
        <ul className="list-group mb-3">
            {transactions.map(transaction => {
                const fromAccount = transaction.fromAccount;
                const toAccount = transaction.toAccount;
                const x =  (transaction.type === "Transfer" && accounts.find(acc => acc.id === fromAccount.id)) ? false : true;
                return (
                    <div key={transaction.id}>
                        <ListCard 
                            id={transaction.id} 
                            title={<b className="fs-4">{transaction.type}</b>} 
                            description={transaction.createdAt} 
                            element={<b className="fs-4">{"$" + transaction.amount}</b>}
                            class={(transaction.type === "Deposit" || transaction.type ===  "Loan" || x) ? "bg-success bg-opacity-25"
                                : (transaction.type === "Withdraw" || !x) ? "bg-danger-subtle" 
                                :  ""} />
                    </div>
                )
            })}
        </ul>
    )
}