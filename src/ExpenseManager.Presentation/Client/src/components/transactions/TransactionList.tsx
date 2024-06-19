import useSWRMutation from "swr/mutation";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "@/components/ui/use-toast.ts";
import {useEffect} from "react";
import {PlusIcon, RefreshCwIcon} from "lucide-react";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Button} from "@/components/ui/button.tsx";
import {TransactionRow} from "@/components/transactions/TransactionRow.tsx";
import {TransactionFilterDialog} from "@/components/transactions/TransactionFilterDialog.tsx";
import {TransactionFormDialog} from "@/components/transactions/TransactionFormDialog.tsx";
import {TabsList, TabsTrigger} from "@/components/ui/tabs.tsx";

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
                <TabsList>
                    <TabsTrigger value="transactions">Transactions</TabsTrigger>
                    <TabsTrigger value="categories">Categories</TabsTrigger>
                </TabsList>

                <div className="flex flex-row gap-x-3">
                    <TransactionFormDialog type={'create'} transaction={{
                        description: '',
                        type: 'Expense',
                        amount: 0,
                        id: '',
                        categoryIds: [],
                        categoryNames: [],
                    }}>
                        <Button
                            variant="default"
                            className="bg-green-500">
                            <PlusIcon className="h-[1.2rem] w-[1.2rem]"/>
                            Add new
                        </Button>
                    </TransactionFormDialog>
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

