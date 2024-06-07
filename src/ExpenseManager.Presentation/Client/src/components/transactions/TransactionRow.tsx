import {TableCell, TableRow} from "@/components/ui/table.tsx";
import {Badge} from "@/components/ui/badge.tsx";
import {Button} from "@/components/ui/button"
import {Pencil, Trash2} from "lucide-react"
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "../ui/dialog";
import {transactionsApiConnector} from "@/api/transactionsApiConnector.ts";
import {useAuth} from "@/components/auth/AuthProvider.tsx";

export function TransactionRow({transaction}: { transaction: TransactionDto }) {
    const {token} = useAuth();
    const {description, type, category, amount, date} = transaction;
    const sign = type === "Income" ? "+" : "-";

    return (
        <TableRow className="items-center odd:bg-accent/20">
            <TableCell>
                <div className="font-medium">{description}</div>
            </TableCell>
            <TableCell className="hidden sm:table-cell">{type}</TableCell>
            <TableCell className="hidden sm:table-cell">
                <Badge className="text-xs" variant="outline">
                    {category}
                </Badge>
            </TableCell>
            <TableCell className="hidden md:table-cell">{date}</TableCell>
            <TableCell className="hidden md:table-cell">{`${sign}\$${amount}`}</TableCell>
            <TableCell className="text-right">
                <div className="flex flex-row gap-x-2 justify-end">
                    <Button variant="outline" size="icon">
                        <Pencil
                            className="h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                    <Dialog>
                        <DialogTrigger asChild>
                            <Button variant="destructive" size="icon">
                                <Trash2
                                    className="h-[1.2rem] w-[1.2rem]"/>
                            </Button>
                        </DialogTrigger>
                        <DialogContent className="sm:max-w-[425px]">
                            <DialogHeader>
                                <DialogTitle>Confirm delete</DialogTitle>
                                <DialogDescription>
                                    Are you sure you want to delete this transaction? This action cannot be undone.
                                </DialogDescription>
                            </DialogHeader>
                            <DialogFooter>
                                <Button type="submit" variant="destructive"
                                        onClick={async () => {
                                            if (!token) return;
                                            await transactionsApiConnector.deleteTransaction(token, transaction)
                                            window.location.reload();
                                        }}
                                >Delete</Button>
                            </DialogFooter>
                        </DialogContent>
                    </Dialog>
                </div>
            </TableCell>
        </TableRow>
    );
}