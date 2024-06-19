import {Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {CalendarIcon} from "lucide-react";
import {Calendar} from "@/components/ui/calendar.tsx";
import {ReactNode, useEffect, useState} from "react";
import {format} from "date-fns";
import {Select, SelectContent, SelectGroup, SelectItem, SelectTrigger, SelectValue} from "@/components/ui/select.tsx";
import useSWR from "swr";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Checkbox} from "@/components/ui/checkbox"
import {CategoryDto} from "@/models/categories/CategoryDto";
import {CheckedState} from "@radix-ui/react-checkbox";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";

interface TransactionFilterDto {
    filters: {
        description?: string,
        transactionType?: 'Expense' | 'Income',
        categoryIds?: string[],
        dateRange?: {
            from: number,
            to: number
        },
        priceRange?: {
            from: number,
            to: number
        }
    }
}

export function TransactionFilterDialog({form, onSubmit, children}: {
    form: any,
    onSubmit: any,
    children: ReactNode
}) {
    const {
        handleSubmit,
        setValue,
        getValues,
        formState: {
            errors
        }
    } = form;

    return (
        <Dialog>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>Filter transactions</DialogTitle>
                    </DialogHeader>
                    <div className="grid gap-3 py-4">
                        <DescriptionInput getValues={getValues} setValue={setValue} errors={errors}/>
                        {/*<AmountInput getValues={getValues} setValue={setValue} errors={errors}/>*/}
                        <TypeSelect getValues={getValues} setValue={setValue} errors={errors}/>
                        <CalendarInput getValues={getValues} setValue={setValue} errors={errors}/>
                        <CategoriesSelect getValues={getValues} setValue={setValue} errors={errors}/>
                    </div>
                    <Button type="submit" className={'w-full'} onClick={() => {
                    }}>
                        Apply filter
                    </Button>
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

    function onChange(value: string) {
        if (value === '' || !isNaN(Number(value)))
            setInputValue(value);
    }

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
                type={"number"}
                value={inputValue}
                onChange={(e) => onChange(e.target.value)}
            />
            {errors.amount &&
                <span className={'text-sm font-medium text-destructive'}>{errors.amount.message}</span>}
        </div>
    )
}

function TypeSelect({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [selectedValue, setSelectedValue] = useState<'Expense' | 'Income' | 'All'>(getValues("transactionType"));

    useEffect(() => {
        const value = selectedValue === 'All' ? undefined : selectedValue;
        setValue("transactionType", value, {
            shouldDirty: true
        });
    }, [selectedValue])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
            <Label htmlFor="type">Type</Label>
            <Select id={'type'} value={selectedValue} onValueChange={setSelectedValue}>
                <SelectTrigger className="w-full">
                    <SelectValue placeholder="Select transaction type"/>
                </SelectTrigger>
                <SelectContent>
                    <SelectGroup>
                        <SelectItem value="All" className={'text-accent'}>All</SelectItem>
                        <SelectItem value="Expense">Expense</SelectItem>
                        <SelectItem value="Income">Income</SelectItem>
                    </SelectGroup>
                </SelectContent>
            </Select>
            {
                errors.type &&
                <span className={'text-sm font-medium text-destructive'}>{errors.type.message}</span>
            }
        </div>
    )
}

function CalendarInput({getValues, setValue, errors}: { getValues: any, setValue: any, errors: any }) {
    const [date, setDate] = useState<FormSchema['dateRange']>(getValues("dateRange"));

    useEffect(() => {
        console.log(date);
        setValue("dateRange", date, {
            shouldDirty: true
        });
    }, [date])

    return (
        <div className="grid w-full max-w-sm items-center gap-1.5">
            <Popover>
                <PopoverTrigger asChild>
                    <Button
                        id="date"
                        variant={"outline"}
                        className={cn(
                            "w-full justify-start text-left font-normal",
                            !date && "text-muted-foreground"
                        )}
                    >
                        <CalendarIcon className="mr-2 h-4 w-4"/>
                        {date?.from ? (
                            date.to ? (
                                <>
                                    {format(date.from, "LLL dd, y")} -{" "}
                                    {format(date.to, "LLL dd, y")}
                                </>
                            ) : (
                                format(date.from, "LLL dd, y")
                            )
                        ) : (
                            <span>Pick a date</span>
                        )}
                    </Button>
                </PopoverTrigger>
                <PopoverContent className="w-auto p-0" align="start">
                    <Calendar
                        initialFocus
                        mode="range"
                        defaultMonth={date?.from}
                        selected={date}
                        onSelect={setDate}
                        numberOfMonths={2}
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
