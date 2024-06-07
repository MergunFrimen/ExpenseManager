import {Card, CardDescription, CardHeader, CardTitle,} from "@/components/ui/card"
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export function TotalBalance({transactions}: { transactions: TransactionDto[] }) {
    const totalBalance = transactions.reduce(
        (acc, transaction) => {
            if (transaction.type === "Income") {
                return acc + transaction.amount;
            } else {
                return acc - transaction.amount;
            }
        }
        , 0);
    const sign = totalBalance >= 0 ? "+" : "-";
    const absTotalBalance = Math.abs(totalBalance);

    return (
        <Card className="w-fit">
            <CardHeader className="px-8">
                <CardDescription>Total balance</CardDescription>
                <CardTitle className="text-4xl">{`${sign}\$${absTotalBalance}`}</CardTitle>
            </CardHeader>
        </Card>
    )
}
