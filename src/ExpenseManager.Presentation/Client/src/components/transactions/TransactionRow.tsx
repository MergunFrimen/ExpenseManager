import {Button} from "@/components/ui/button.tsx";
import {Pencil} from "lucide-react";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {Badge} from "@/components/ui/badge.tsx";
import {RemoveTransactionDialog} from "@/components/transactions/RemoveTransactionDialog.tsx";
import {TransactionFormDialog} from "@/components/transactions/TransactionFormDialog.tsx";

export function TransactionRow({transaction}: { transaction: TransactionDto }) {
    let date: string | Date = '';
    if (transaction.date) {
        date = new Date(transaction.date * 1000)
        date = date.toLocaleDateString()
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
                {transaction.type === 'Expense' && <span className={'text-red-500'}>{amount}</span>}
                {transaction.type === 'Income' && <span className={'text-green-500'}>{amount}</span>}
            </h1>
            <h1 className={'text-left w-[10px]'}>{date}</h1>
            <div className={'flex flex-row wrap gap-1 p-2 justify-end'}>
                {
                    transaction.categoryNames.map(name => (
                        <Badge key={name} variant={'outline'} className={'min-w-fit'}>{name}</Badge>
                    ))
                }
            </div>
            <div className="flex flex-row gap-x-1 justify-end">
                <TransactionFormDialog type={'edit'} transaction={transaction}>
                    <Button variant="secondary" size="icon">
                        <Pencil className="h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                </TransactionFormDialog>
                <RemoveTransactionDialog transaction={transaction}/>
            </div>
        </div>
    )
}
