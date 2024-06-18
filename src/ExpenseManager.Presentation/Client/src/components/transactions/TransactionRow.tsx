import {Dialog, DialogContent, DialogTrigger} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useState} from "react";
import {Pencil, Trash2} from "lucide-react";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {Badge} from "@/components/ui/badge.tsx";
import {RemoveTransactionDialog} from "@/components/transactions/RemoveTransactionDialog.tsx";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {EditCategoryForm} from "@/components/categories/EditCategoryForm.tsx";
import {RemoveCategoryDialog} from "@/components/categories/RemoveCategoryDialog.tsx";
import {EditTransactionForm} from "@/components/transactions/EditTransactionForm.tsx";

export function TransactionRow({transaction}: { transaction: TransactionDto }) {
    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState<'edit' | 'remove' | undefined>(undefined);

    let date: string | Date = '';
    if (transaction.date) {
        date = new Date(transaction.date)
        date = `${date.getDay()}.${date.getMonth()}.${date.getFullYear()}`
    }

    function numberFormat(number: number) {
        let formatted = number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        if (transaction.type === 'Expense')
            return `-${formatted} CZK`
        else
            return `+${formatted} CZK`
    }

    const amount = numberFormat(transaction.amount);

    return (
        <div key={transaction.id} className={'grid grid-cols-5 items-center space-y-3'}>
            <h1 className={'text-left'}>{transaction.description}</h1>
            <h1 className={'text-right w-[100px]'}>
                {transaction.type === 'Expense' && <h1 className={'text-red-500'}>{amount}</h1>}
                {transaction.type === 'Income' && <h1 className={'text-green-500'}>{amount}</h1>}
            </h1>
            <h1 className={'text-left w-[10px]'}>{date}</h1>
            <div className={'flex flex-row wrap gap-1 p-2 justify-end'}>
                {
                    transaction.categoryNames.map(name => (
                        <Badge variant={'outline'} className={'min-w-fit'}>{name}</Badge>
                    ))
                }
            </div>
            <div className="flex flex-row gap-x-1 justify-end">
                <Dialog open={open} onOpenChange={setOpen}>
                    <DialogTrigger asChild>
                        <Button variant="secondary" size="icon"
                                onClick={() => setDialogType('edit')}>
                            <Pencil className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogTrigger asChild>
                        <Button variant="destructive" size="icon"
                                onClick={() => setDialogType('remove')}>
                            <Trash2 className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                        <TransactionDialogContext transaction={transaction} type={dialogType} setOpen={setOpen}/>
                    </DialogContent>
                </Dialog>
            </div>
        </div>
    )
}

function TransactionDialogContext({type, transaction, setOpen}: {
    type: 'edit' | 'remove' | undefined,
    transaction: TransactionDto,
    setOpen: (open: boolean) => void
}) {
    if (type === 'edit') {
        return <EditTransactionForm type={'edit'} transaction={transaction} setOpen={setOpen}/>
    }
    if (type === 'remove') {
        return <RemoveTransactionDialog transaction={transaction} setOpen={setOpen}/>
    }
    return <></>
}