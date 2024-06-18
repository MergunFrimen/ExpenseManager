import {Dialog, DialogContent, DialogTrigger} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useState} from "react";
import {Pencil, Trash2} from "lucide-react";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export function TransactionRow({transaction}: { transaction: TransactionDto }) {
    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState<'edit' | 'remove' | undefined>(undefined);

    let date: string | Date = '';
    if (transaction.date)
        date = new Date(transaction.date).toLocaleDateString('cz-CZ')

    return (
        <div key={transaction.id} className={'grid grid-cols-5 items-center space-y-3'}>
            <h1>{transaction.description}</h1>
            <h1>{transaction.amount}</h1>
            <h1>{date}</h1>
            <h1>{transaction.categoryNames.join(', ')}</h1>
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
                        {/*<CategoryDialogContext category={transaction} type={dialogType} setOpen={setOpen}/>*/}
                    </DialogContent>
                </Dialog>
            </div>
        </div>
    )
}

// function CategoryDialogContext({type, category, setOpen}: {
//     type: 'edit' | 'remove' | undefined,
//     category: CategoryDto,
//     setOpen: (open: boolean) => void
// }) {
//     if (type === 'edit') {
//         return <CategoryForm type={'edit'} category={category} setOpen={setOpen}/>
//     }
//     if (type === 'remove') {
//         return <RemoveCategoryDialog category={category} setOpen={setOpen}/>
//     }
//     return <></>
// }
