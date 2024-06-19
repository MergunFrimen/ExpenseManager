import useSWRMutation from "swr/mutation";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "@/components/ui/use-toast.ts";
import {useEffect} from "react";
import {FilterIcon, PlusIcon, RefreshCwIcon} from "lucide-react";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Button} from "@/components/ui/button.tsx";
import {TransactionRow} from "@/components/transactions/TransactionRow.tsx";
import {TransactionFormDialog} from "@/components/transactions/TransactionFormDialog.tsx";
import {TabsList, TabsTrigger} from "@/components/ui/tabs.tsx";
import {TransactionFilterDialog} from "@/components/transactions/TransactionFilterDialog.tsx";
import { TransactionDto } from "@/models/transactions/TransactionDto.ts";

const formSchema = z.object({
    description: z.string().max(150).optional(),
    transactionType: z.enum(["Expense", "Income"]).optional(),
    categoryIds: z.array(z.string()).optional(),
    dateRange: z.object({
        from: z.date().optional(),
        to: z.date().optional()
    }).optional(),
    priceRange: z.object({
        from: z.date().optional(),
        to: z.date().optional()
    }).optional()
})

type FormSchema = z.infer<typeof formSchema>;

async function transactionFetcher(url: string, token: string | null, {arg}: {
    arg: { filters: { name?: string } }
}) {
    const response = await fetch(`${url}/search`, {
        method: 'POST',
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
    const form = useForm<FormSchema>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            description: undefined,
            transactionType: undefined,
            categoryIds: [],
            dateRange: {},
            priceRange: {},
        }
    })
    const {
        data,
        trigger,
        error
    } = useSWRMutation(
        ['/api/v1/transactions', token],
        ([url, token], arg) => transactionFetcher(url, token, arg),
        {}
    );

    function onSubmit(data: FormSchema) {
        const dataFrom = data.dateRange?.from ? Math.floor(data.dateRange.from.getTime() / 1000) : undefined;
        const dataTo = data.dateRange?.to ? Math.floor(data.dateRange.to.getTime() / 1000) : undefined;

        const request = {
            filters: {
                ...data,
                dateRange: data.dateRange ? {
                    from: dataFrom,
                    to: dataTo
                } : {},
                categoryIds: data.categoryIds?.length && data.categoryIds?.length > 0 ? data.categoryIds : undefined
            }
        }

        toast({
            title: "Filtered transactions with the following values:",
            description: (
                <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
          <code className="text-white">
              {JSON.stringify(request, null, 2)}
          </code>
        </pre>
            ),
        })

        trigger(request as any);
    }

    useEffect(() => {
        trigger({filters: {}} as any);
    }, []);

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

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
                    <TransactionFilterDialog form={form} onSubmit={onSubmit}>
                        <Button variant="secondary" size="icon">
                            <FilterIcon className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </TransactionFilterDialog>
                    <Button variant="ghost" size="icon" onClick={
                        () => trigger({filters: {}} as any)
                    }>
                        <RefreshCwIcon className="h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                </div>
            </div>
            <ScrollArea className={'size-full h-[670px] outline outline-1 outline-accent rounded-md px-5'}>
                {data && data.sort((x: TransactionDto, y: TransactionDto) => x.id > y.id ? 1 : -1).map((transaction: TransactionDto) =>
                    <TransactionRow key={transaction.id} transaction={transaction}/>
                )}
            </ScrollArea>
        </div>
    );
}

