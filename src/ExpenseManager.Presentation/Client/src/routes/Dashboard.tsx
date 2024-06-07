import BaseLayout from "@/layouts/BaseLayout.tsx";
import TransactionTable from "@/components/transactions/TransactionTable.tsx";
import {TotalBalance} from "@/components/transactions/TotalBalance.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useEffect, useState} from "react";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {transactionsApiConnector} from "@/api/transactionsApiConnector.ts";

export default function Dashboard() {
    const {token} = useAuth();
    const [transactions, setTransactions] = useState<TransactionDto[]>([])

    useEffect(() => {
        async function fetchTransactions() {
            if (!token) return;

            const transactions = await transactionsApiConnector.getTransactions(token);
            setTransactions(transactions);
        }

        fetchTransactions();
    }, [token]);

    return <BaseLayout>
        <div className="container flex flex-col size-full gap-y-3">
            <TotalBalance transactions={transactions}/>
            <TransactionTable transactions={transactions}/>
        </div>
    </BaseLayout>
}