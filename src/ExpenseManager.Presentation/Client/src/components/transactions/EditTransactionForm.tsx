import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from "@/components/ui/form.tsx";
import {Input} from "@/components/ui/input.tsx";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "../ui/use-toast";
import useSWRMutation from "swr/mutation";
import {useEffect, useState} from "react";
import {z} from "zod";
import {Button} from "@/components/ui/button.tsx";
import {DialogFooter, DialogHeader, DialogTitle} from "@/components/ui/dialog.tsx";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {Calendar} from "@/components/ui/calendar.tsx";
import {CalendarIcon} from "lucide-react";
import {format} from "date-fns";
import {
    DropdownMenu,
    DropdownMenuCheckboxItem,
    DropdownMenuContent,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";

const formSchema = z.object({
    description: z.string().min(1).max(150),
    amount: z.coerce.number().gte(0),
    type: z.enum(['Expense', 'Income']),
    date: z.date(),
    categoryIds: z.array(z.number())
})

type FormFields = z.infer<typeof formSchema>;

async function fetcher(url: string, token: string | null, {arg}: { arg: { filters: { name?: string } } }) {
    const response = await fetch(url, {
        method: "PUT",
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

export function EditTransactionForm({type, transaction, setOpen}: {
    type: 'create' | 'edit',
    transaction: TransactionDto,
    setOpen: (open: boolean) => void
}) {
    const {token} = useAuth();
    const {toast} = useToast()
    const form = useForm<FormFields>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            description: transaction.description
        },
    })
    const {
        trigger,
        error
    } = useSWRMutation(
        [`/api/v1/transactions/${transaction.id}`, token],
        ([url, token], arg) => fetcher(url, token, arg),
        {}
    );
    const [date, setDate] = useState<Date>()

    function onSubmit(e: FormFields) {
        trigger(e);
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
        console.log(date);
    }, [date]);


    return (
        <div className="flex flex-col gap-y-3 p-2">
            <DialogHeader>
                <DialogTitle>Edit transaction</DialogTitle>
            </DialogHeader>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}
                      className="space-y-2 flex flex-col size-full items-center justify-center">
                    <FormField
                        control={form.control}
                        name="description"
                        render={({field}) => (
                            <FormItem className="w-full">
                                <FormLabel>Description</FormLabel>
                                <FormControl>
                                    <Input placeholder="Transaction description" {...field}/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="amount"
                        render={({field}) => (
                            <FormItem className="w-full">
                                <FormLabel>Amount</FormLabel>
                                <FormControl>
                                    <Input placeholder="0.00" {...field}/>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="date"
                        render={({field}) => (
                            <FormItem className="w-full">
                                <FormLabel>Date</FormLabel>
                                <FormControl>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <Button
                                                variant={"outline"}
                                                className={cn(
                                                    "w-full justify-start text-left font-normal",
                                                    !date && "text-muted-foreground"
                                                )}
                                            >
                                                <CalendarIcon className="mr-2 h-4 w-4"/>
                                                {date ? format(date, "PPP") : <span>Pick a date</span>}
                                            </Button>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-auto p-0">
                                            <Calendar
                                                mode="single"
                                                selected={date}
                                                onSelect={setDate}
                                                initialFocus
                                            />
                                        </PopoverContent>
                                    </Popover>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <FormField
                        control={form.control}
                        name="type"
                        render={({field}) => (
                            <FormItem className="w-full">
                                <FormLabel>Type</FormLabel>
                                <FormControl>
                                    <DropdownMenu>
                                        <DropdownMenuTrigger asChild>
                                            <Button variant="outline">Open</Button>
                                        </DropdownMenuTrigger>
                                        <DropdownMenuContent className="w-56">
                                            <DropdownMenuLabel>Appearance</DropdownMenuLabel>
                                            <DropdownMenuSeparator/>
                                            <DropdownMenuCheckboxItem
                                                checked={showStatusBar}
                                                onCheckedChange={setShowStatusBar}
                                            >
                                                Status Bar
                                            </DropdownMenuCheckboxItem>
                                            <DropdownMenuCheckboxItem
                                                checked={showActivityBar}
                                                onCheckedChange={setShowActivityBar}
                                                disabled
                                            >
                                                Activity Bar
                                            </DropdownMenuCheckboxItem>
                                            <DropdownMenuCheckboxItem
                                                checked={showPanel}
                                                onCheckedChange={setShowPanel}
                                            >
                                                Panel
                                            </DropdownMenuCheckboxItem>
                                        </DropdownMenuContent>
                                    </DropdownMenu> </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <Button type="submit" onClick={() => {
                        // setOpen(false)
                    }}>
                        Submit
                    </Button>
                </form>
            </Form>
            <DialogFooter>
            </DialogFooter>
        </div>
    );
}
