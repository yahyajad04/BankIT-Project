import { useState , useEffect } from "react";
import { getAccount } from "../../accounts/api/accountsApi";
import getTransactions from "../api/transactionsApi";

export function useTransactions(token, accountId) {
    const [loading, setLoading] = useState(false);
    const [account, setAccount] = useState(null);
    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                setLoading(true);

                const [accountData, transactionData] = await Promise.all([
                    getAccount(token, accountId),
                    getTransactions(token, accountId)
                ]);

                setAccount(accountData);
                setTransactions(transactionData);
            } finally {
                setLoading(false);
            }
        }

        fetchData();
    }, [token, accountId]);

    return {
        loading,
        account,
        transactions
    };
}