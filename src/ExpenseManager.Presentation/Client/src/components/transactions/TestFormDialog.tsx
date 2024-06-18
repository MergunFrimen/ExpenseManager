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
import {SubmitHandler, useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {CalendarIcon} from "lucide-react";
import {Calendar} from "@/components/ui/calendar.tsx";
import {useEffect, useState} from "react";
import {format} from "date-fns";
import {Select, SelectContent, SelectGroup, SelectItem, SelectTrigger, SelectValue} from "@/components/ui/select.tsx";
import {toast} from "../ui/use-toast";

const formSchema = z.object({
    description: z.string().min(1).max(150),
    amount: z.coerce.number().gte(0),
    type: z.enum(["Expense", "Income"]),
    date: z.date().optional(),
})

type FormSchema = z.infer<typeof formSchema>;

export function TestFormDialog() {
    const form = useForm<FormSchema>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            description: "",
            amount: 0,
        }
    })
    const {
        handleSubmit,
        setValue,
        formState: {
            errors
        }
    } = form;

    function onSubmit(data: FormSchema) {
        console.log(data)
        toast({
            title: "You submitted the following values:",
            description: (
                <pre className="mt-2 w-[340px] rounded-md bg-slate-950 p-4">
          <code className="text-white">{JSON.stringify(data, null, 2)}</code>
        </pre>
            ),
        })
    }

    return (
        <Dialog open={true}>
            <DialogTrigger asChild>
                <Button variant="outline">Edit Profile</Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]">
                <form onSubmit={handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>Edit transaction</DialogTitle>
                    </DialogHeader>
                    <div className="grid gap-3 py-4">
                        <DescriptionInput setValue={setValue} errors={errors}/>
                        <AmountInput setValue={setValue} errors={errors}/>
                        <TypeSelect setValue={setValue} errors={errors}/>
                        <CalendarInput setValue={setValue} errors={errors}/>
                    </div>
                    <DialogFooter>
                        <Button type="submit">Save changes</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    );
}

function DescriptionInput({setValue, errors}: { setValue: any, errors: any }) {
    const [inputValue, setInputValue] = useState<string>('')

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

function AmountInput({setValue, errors}: { setValue: any, errors: any }) {
    const [inputValue, setInputValue] = useState<string>('')

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

function TypeSelect({setValue, errors}: { setValue: any, errors: any }) {
    const [selectedValue, setSelectedValue] = useState<string>('')

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

function CalendarInput({setValue, errors}: { setValue: any, errors: any }) {
    const [date, setDate] = useState<Date>()

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

function CategoriesSelect({setValue, errors}: { setValue: any, errors: any }) {
    const [selectedValue, setSelectedValue] = useState<string>('')

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
