import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {Table, TableBody, TableHead, TableHeader, TableRow} from "@/components/ui/table";
import {TransactionItem} from "@/components/transactions/TransactionItem.tsx";

export default function TransactionTable() {
    return (
        <Card className="size-full">
            <CardHeader className="px-7">
                <CardTitle>Transactions</CardTitle>
                {/*<CardDescription>Recent orders from your store.</CardDescription>*/}
            </CardHeader>
            <CardContent>
                <Table>
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
                    <TableBody className="odd:bg-accent">
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                        <TransactionItem/>
                    </TableBody>
                </Table>
            </CardContent>
        </Card>
    )
}
