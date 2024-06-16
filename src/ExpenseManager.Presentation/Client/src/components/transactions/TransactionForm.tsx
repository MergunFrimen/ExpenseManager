import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import BaseLayout from "@/layouts/BaseLayout.tsx";
import {CalendarIcon, Check, ChevronsUpDown, PlusIcon} from "lucide-react";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {Calendar} from "@/components/ui/calendar.tsx";
import {
    Dialog,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from "@/components/ui/command.tsx";
import useSWR from 'swr'
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

const schema = z.object({
    description: z.string().max(150),
    amount: z.number().min(0.01),
    type: z.enum(["Expense", "Income"]),
    data: z.date(),
    categoryIds: z.array(z.string())
});

async function postRequest(url: string, { arg }: { arg: TransactionDto }) {
    return fetch(url, {
        method: 'POST',
        body: JSON.stringify(arg)
    }).then(res => res.json())
}

export function TransactionForm() {
    const form = useForm<z.infer<typeof schema>>({
        resolver: zodResolver(schema),
    })

    const { data, trigger: createTransaction, isMutating: isCreating } = useSWR('/api/v1/transactions', postRequest)

    return (
        <BaseLayout>
            <Dialog open={true}>
                <DialogTrigger asChild>
                    <Button className="w-fit" variant="outline">Add new expense</Button>
                </DialogTrigger>
                <DialogContent className="sm:max-w-[425px]">
                    <DialogHeader>
                        <DialogTitle>Add new expense</DialogTitle>
                    </DialogHeader>
                    <div className="grid gap-4">
                        <div className="grid gap-2">
                            <Label htmlFor="description">Description</Label>
                            <Input
                                id="description"
                                name="description"
                                type="text"
                                placeholder="Description"
                                required
                            />
                        </div>
                        <div className="grid gap-2">
                            <Label htmlFor="amount">Amount</Label>
                            <Input
                                id="amount"
                                name="amount"
                                type="text"
                                placeholder={"123.45"}
                                required
                            />
                        </div>

                        <Popover open={open} onOpenChange={() => {
                            setOpen(!open)

                        }}>
                            <PopoverTrigger asChild>
                                <Button
                                    variant="outline"
                                    role="combobox"
                                    aria-expanded={open}
                                    className="justify-between"
                                >
                                    {value
                                        ? categories.find((category) => category.value === value)?.label
                                        : "Select category..."}
                                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50"/>
                                </Button>
                            </PopoverTrigger>
                            <PopoverContent className="p-0">
                                <Command>
                                    <CommandInput placeholder="Search category..." value={written}
                                                  onValueChange={setWritten}/>
                                    <CommandList>
                                        <CommandEmpty>
                                            <Button type="button" variant="ghost"
                                                    className="flex flex-row gap-x-2 px-2 w-full justify-center items-center">
                                                <PlusIcon className="h-4 w-4"/>
                                                <span className="">Create "{written}" category</span>
                                            </Button>
                                        </CommandEmpty>
                                        <CommandGroup>
                                            {categories.map((category) => (
                                                <CommandItem
                                                    key={category.value}
                                                    value={category.value}
                                                    onSelect={(currentValue) => {
                                                        setValue(currentValue === value ? "" : currentValue)
                                                        setOpen(false)
                                                    }}
                                                >
                                                    <Check
                                                        className={cn(
                                                            "mr-2 h-4 w-4",
                                                            value === category.value ? "opacity-100" : "opacity-0"
                                                        )}
                                                    />
                                                    {category.label}
                                                </CommandItem>
                                            ))}
                                        </CommandGroup>
                                    </CommandList>
                                </Command>
                            </PopoverContent>
                        </Popover>

                        <Popover>
                            <PopoverTrigger asChild>
                                <Button
                                    variant={"outline"}
                                    className={cn(
                                        "justify-start text-left font-normal",
                                        // !date && "text-muted-foreground"
                                    )}
                                >
                                    <CalendarIcon className="mr-2 h-4 w-4"/>
                                    <span>Pick a date</span>
                                </Button>
                            </PopoverTrigger>
                            <PopoverContent className="w-auto p-0">
                                <Calendar
                                    mode="single"
                                    // selected={date}
                                    // onSelect={setDate}
                                    initialFocus
                                />
                            </PopoverContent>
                        </Popover>
                    </div>
                    <DialogFooter>
                        <Button
                            disabled={isCreating}
                            onClick={async () => {
                                try {
                                    const transactionDto: TransactionDto = {
                                        "id": "",
                                        "description": "test",
                                        "amount": 50.00,
                                        "type": "Expense",
                                        "date": 1718471967,
                                        "categoryIds": [
                                        ]
                                    }
                                    const result = await createTransaction(transactionDto)
                                    console.log(result)
                                } catch (e) {
                                    // error handling
                                }
                            }}
                            type="submit" className="w-full">
                            Add expense
                        </Button>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </BaseLayout>
    )

}
