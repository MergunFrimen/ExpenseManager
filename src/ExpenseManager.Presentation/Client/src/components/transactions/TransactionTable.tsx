import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {Table, TableBody, TableHead, TableHeader, TableRow} from "@/components/ui/table";
import {TransactionRow} from "@/components/transactions/TransactionRow.tsx";
import {useEffect, useState} from "react";
import {TransactionDto} from "@/components/models/TransactionDto.ts";
import {apiConnector} from "@/api/apiConnector.ts";

export default function TransactionTable() {
    const [transactions, setTransactions] = useState<TransactionDto[]>([])

    useEffect(() => {
        async function fetchTransactions() {
            const transactions = await apiConnector.getTransactions("1");
            setTransactions(transactions);
        }
        fetchTransactions();
    }, []);
    
    return (
        <Card className="p-5 h-[500px]">
            <CardHeader className="px-7">
                <CardTitle>Transactions</CardTitle>
                {/*<CardDescription>Recent orders from your store.</CardDescription>*/}
            </CardHeader>
            <CardContent className="">
                <Table className="">
                    <TableHeader>
                        <TableRow>
                            <TableHead>Description</TableHead>
                            <TableHead className="hidden sm:table-cell">Type</TableHead>
                            <TableHead className="hidden sm:table-cell">Category</TableHead>
                            <TableHead className="hidden md:table-cell">Date</TableHead>
                            <TableHead className="hidden md:table-cell">Amount</TableHead>
                            <TableHead className="text-right"></TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody className="">
                        {transactions.map((transaction) => (
                            <TransactionRow key={transaction.id} transaction={transaction}/>
                        ))}
                    </TableBody>
                </Table>
            </CardContent>
        </Card>
    )
}
