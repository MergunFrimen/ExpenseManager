import {
    Dialog,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {CalendarIcon} from "lucide-react";
import {Calendar} from "@/components/ui/calendar.tsx";
import {useEffect, useState} from "react";
import {format} from "date-fns";
import {Select, SelectContent, SelectGroup, SelectItem, SelectTrigger, SelectValue} from "@/components/ui/select.tsx";
import {toast} from "../ui/use-toast";
import useSWR from "swr";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Checkbox} from "@/components/ui/checkbox"
import {CategoryDto} from "@/models/categories/CategoryDto";
import {CheckedState} from "@radix-ui/react-checkbox";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import useSWRMutation from "swr/mutation";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

async function transactionFetcher(url: string, token: string | null, method: 'POST' | 'PUT', {arg}: {
    arg: { filters: { name?: string } }
}) {
    const response = await fetch(url, {
        method: method,
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

const formSchema = z.object({
    description: z.string().min(1).max(150),
    amount: z.coerce.number().gte(0),
    type: z.enum(["Expense", "Income"]),
    date: z.date().optional(),
    categoryIds: z.array(z.string())
})

type FormSchema = z.infer<typeof formSchema>;

export function TestFormDialog({type, transaction}: { type: 'create' | 'edit', transaction: TransactionDto }) {
    const {token} = useAuth();
    const form = useForm<FormSchema>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            description: type === 'edit' ? transaction.description : undefined,
            amount: type === 'edit' ? transaction.amount : undefined,
            type: type === 'edit' ? transaction.type : undefined,
            date: type === 'edit' && transaction.date ? new Date(transaction.date * 1000) : undefined,
            categoryIds: type === 'edit' ? transaction.categoryIds : []
        }
    })
    const {
        handleSubmit,
        setValue,
        getValues,
        formState: {
            errors
        }
    } = form;
    const {
        trigger: createTrigger,
        error: createError
    } = useSWRMutation(
        ['/api/v1/transactions', token],
        ([url, token], arg) => transactionFetcher(url, token, 'POST', arg),
        {}
    );
    const {
        trigger: updateTrigger,
        error: updateError
    } = useSWRMutation(
        [`/api/v1/transactions/${transaction.id}`, token],
        ([url, token], arg) => transactionFetcher(url, token, 'PUT', arg),
        {}
    );

    function onSubmit(data: FormSchema) {
        toast({
            title: type === 'create' ?
                "Created transaction with the following values:" :
                "Updated transaction with the following values:",
            description: (
                <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
          <code className="text-white">
              {JSON.stringify(data, null, 2)}
          </code>
        </pre>
            ),
        })

        const request = {
            ...data,
            date: data.date ? Math.floor(data.date / 1000) : null
        }

        if (type === 'create') {
            createTrigger(request);
        } else {
            updateTrigger(request);
        }
    }

    useEffect(() => {
        if (createError)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [createError, updateError]);

    return (
        <Dialog defaultOpen={true}>
            <DialogTrigger asChild>
                <Button variant="outline">Edit Profile</Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>Edit transaction</DialogTitle>
                    </DialogHeader>
                    <div className="grid gap-3 py-4">
                        <DescriptionInput getValues={getValues} setValue={setValue} errors={errors}/>
                        <AmountInput getValues={getValues} setValue={setValue} errors={errors}/>
                        <TypeSelect getValues={getValues} setValue={setValue} errors={errors}/>
                        <CalendarInput getValues={getValues} setValue={setValue} errors={errors}/>
                        <CategoriesSelect getValues={getValues} setValue={setValue} errors={errors}/>
                    </div>
                    <DialogFooter>
                        <Button type="submit">Save changes</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    );
}

function DescriptionInput({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [inputValue, setInputValue] = useState<string>(getValues("description"));

    useEffect(() => {
        setValue("description", inputValue, {
            shouldDirty: true
        });
    }, [inputValue])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
            <Label htmlFor="description">Description</Label>
            <Input
                id="description"
                className="col-span-3"
                autoComplete={"off"}
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
            />
            {errors.description &&
                <span className={'text-sm font-medium text-destructive'}>{errors.description.message}</span>}
        </div>
    )
}

function AmountInput({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [inputValue, setInputValue] = useState<string>(getValues("amount"));

    useEffect(() => {
        setValue("amount", inputValue, {
            shouldDirty: true
        });
    }, [inputValue])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
            <Label htmlFor="amount">Amount</Label>
            <Input
                id="amount"
                className="col-span-3"
                autoComplete={"off"}
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
            />
            {errors.amount &&
                <span className={'text-sm font-medium text-destructive'}>{errors.amount.message}</span>}
        </div>
    )
}

function TypeSelect({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [selectedValue, setSelectedValue] = useState<string>(getValues("type"));

    useEffect(() => {
        setValue("type", selectedValue, {
            shouldDirty: true
        });
    }, [selectedValue])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
            <Select value={selectedValue} onValueChange={setSelectedValue}>
                <SelectTrigger className="w-full">
                    <SelectValue placeholder="Select transaction type"/>
                </SelectTrigger>
                <SelectContent>
                    <SelectGroup>
                        <SelectItem value="Expense">Expense</SelectItem>
                        <SelectItem value="Income">Income</SelectItem>
                    </SelectGroup>
                </SelectContent>
            </Select>
            {errors.type &&
                <span className={'text-sm font-medium text-destructive'}>{errors.type.message}</span>}
        </div>
    )
}

function CalendarInput({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [date, setDate] = useState<Date>(getValues("date"));

    useEffect(() => {
        setValue("date", date, {
            shouldDirty: true
        });
    }, [date])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
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
            {errors.date &&
                <span className={'text-sm font-medium text-destructive'}>{errors.date.message}</span>}
        </div>
    )
}

async function fetcher(url: string, token: string | null) {
    const response = await fetch(`${url}/search`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({filters: {name: ''}})
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

function CategoriesSelect({getValues, setValue}: { getValues: any, setValue: any, errors: any }) {
    const {token} = useAuth();
    const {data} = useSWR(['/api/v1/categories', token], ([url, token]) => fetcher(url, token));

    const [selectedValues, setSelectedValues] = useState<string[]>(getValues("categoryIds"));
    const [open, setOpen] = useState<boolean>(false);

    function onCheckedChange(checked: CheckedState, category: CategoryDto) {
        if (checked)
            setSelectedValues([...selectedValues, category.id]);
        else
            setSelectedValues(selectedValues.filter((value) => value !== category.id))
    }

    useEffect(() => {
        setValue("categoryIds", selectedValues, {
            shouldDirty: true
        });
    }, [selectedValues])

    return (
        <Popover>
            <PopoverTrigger onClick={() => setOpen(!open)}>
                <Button type={'button'} className="w-full" variant={'outline'}>
                    Select categories
                </Button>
            </PopoverTrigger>
            <PopoverContent className={'w-full'}>
                <ScrollArea className="h-[250px] w-full rounded-md">
                    {
                        data && data.map((category: CategoryDto) =>
                            <div key={category.id}
                                 className={'relative space-x-2 flex cursor-default select-none items-center rounded-sm px-2 py-1.5 text-sm outline-none transition-colors focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50'}
                            >
                                <Checkbox
                                    checked={selectedValues.includes(category.id)}
                                    onCheckedChange={(checked) => onCheckedChange(checked, category)}
                                />
                                <Label
                                    htmlFor={category.id}
                                    className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                                >
                                    {category.name}
                                </Label>
                            </div>
                        )
                    }
                </ScrollArea>
            </PopoverContent>
        </Popover>
    )
}
