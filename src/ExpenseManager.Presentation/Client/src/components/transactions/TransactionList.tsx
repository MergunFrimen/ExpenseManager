import useSWRMutation from "swr/mutation";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "@/components/ui/use-toast.ts";
import {useEffect, useState} from "react";
import {PlusIcon, RefreshCwIcon} from "lucide-react";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Button} from "@/components/ui/button.tsx";
import {TransactionRow} from "@/components/transactions/TransactionRow.tsx";
import {TransactionFilterDialog} from "@/components/transactions/TransactionFilterDialog.tsx";
import {Dialog, DialogContent, DialogTrigger} from "@/components/ui/dialog.tsx";
import {CreateCategoryForm} from "@/components/categories/CreateCategoryForm.tsx";

const formSchema = z.object({
    description: z.string(),
    type: z.enum(['Expense', 'Income']),
    dateFrom: z.date(),
    dateTo: z.date(),
    priceFrom: z.number().min(0),
    priceTo: z.number().min(0),
})

async function fetcher(url: string, token: string | null, {arg}: { arg: { filters: { name?: string } } }) {
    const response = await fetch(`${url}/search`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify(arg)
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function TransactionList() {
    const {token} = useAuth();
    const {toast} = useToast()
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            description: '',
            dateFrom: new Date(Date.now()),
            dateTo: new Date(Date.now()),
            priceFrom: 0,
            priceTo: 0,
        },
    })
    const {
        data,
        trigger,
        error
    } = useSWRMutation(['/api/v1/transactions', token], ([url, token], arg) => fetcher(url, token, arg));
    const [open, setOpen] = useState(false);

    function onSubmit(e) {
        trigger({filters: e});
    }

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

    useEffect(() => {
        trigger({filters: {}});
    }, []);

    return (
        <div className="flex flex-col gap-y-3">
            <div className="flex flex-row gap-x-3 justify-between">
                <Dialog open={open} onOpenChange={setOpen}>
                    <DialogTrigger asChild>
                        <Button
                            variant="default"
                            className="bg-green-500"
                            onClick={
                                () => trigger({filters: {}})
                            }>
                            <PlusIcon className="h-[1.2rem] w-[1.2rem]"/>
                            Add new
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                        <CreateCategoryForm setOpen={setOpen}/>
                    </DialogContent>
                </Dialog>

                <div className="flex flex-row gap-x-3">
                    <TransactionFilterDialog form={form} onSubmit={onSubmit}/>
                    <Button variant="ghost" size="icon" onClick={
                        () => trigger({filters: {}})
                    }>
                        <RefreshCwIcon className="h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                </div>
            </div>
            <ScrollArea className={'size-full h-[600px] outline outline-1 outline-accent rounded-md px-5'}>
                {data && data.sort((x, y) => x.id > y.id ? 1 : -1).map(transaction =>
                    <TransactionRow key={transaction.id} transaction={transaction}/>
                )}
            </ScrollArea>
        </div>
    );
}

