import {Link, useNavigate, useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {transactionsApiConnector} from "@/api/transactionsApiConnector.ts";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Card, CardContent, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import BaseLayout from "@/layouts/BaseLayout.tsx";
import {categoriesApiConnector} from "@/api/categoriesApiConnector.ts";
import {
    DropdownMenu,
    DropdownMenuCheckboxItem,
    DropdownMenuContent,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {ArrowBigLeft, CalendarIcon, CirclePlus, FilterIcon} from "lucide-react";
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover.tsx";
import {cn} from "@/lib/utils.ts";
import {Calendar} from "@/components/ui/calendar.tsx";
import {format} from "date-fns";

export function TransactionForm({type}: { type: "create" | "update" }) {
    const {id} = useParams();
    const {token} = useAuth();
    const navigate = useNavigate();
    const [categories, setCategories] = useState([]);
    const [date, setDate] = useState<Date>()

    const [transaction, setTransaction] = useState<TransactionDto>({
        id: "",
        categoryId: "",
        type: "",
        category: "",
        description: "",
        amount: 0,
        date: ""
    });

    useEffect(() => {
        async function fetchCategories() {
            if (!token) return;

            const categories = await categoriesApiConnector.getCategories(token);

            setCategories(categories);
        }

        fetchCategories();
    }, [token]);


    useEffect(() => {
        async function fetchTransaction() {
            if (!token || !id) return;
            

            const trans = await transactionsApiConnector.getTransaction(token, id)
            setTransaction(trans);
        }

        fetchTransaction();
    }, [id]);

    function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
        const {name, value} = event.target;
        setTransaction({...transaction, [name]: value});
    }

    useEffect(() => {
        console.log(transaction);
    }, [transaction]);

    useEffect(() => {
        const newTransaction = {...transaction, date: date ? format(date, "yyyy-MM-dd") : ""};
        setTransaction(newTransaction);
    }, [date])


    const action = type === "create" ? "Create" : "Update";

    return (
        <BaseLayout>
            <div className="container flex flex-col size-full gap-y-3 justify-center items-center">
                <div className="flex gap-2 w-[400px] items-center">
                    <Button variant="outline" size="icon" onClick={() => navigate("/app")}>
                        <ArrowBigLeft
                            className="absolute h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                    <h2>Back</h2>
                </div>

                <Card className="mx-auto w-[400px]">
                    <CardHeader>
                        <CardTitle className="text-2xl">{action} Transaction</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="grid gap-4">
                            <div className="grid gap-2">
                                <Label htmlFor="description">Description</Label>
                                <Input
                                    id="description"
                                    name="description"
                                    type="text"
                                    required
                                    value={transaction.description}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="amount">Amount</Label>
                                <Input
                                    id="amount"
                                    name="amount"
                                    type="text"
                                    required
                                    value={transaction.amount}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="categoryId">CategoryId</Label>
                                <Input
                                    id="categoryId"
                                    name="categoryId"
                                    type="text"
                                    required
                                    value={transaction.categoryId}
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="type">Type</Label>
                                <Input
                                    id="type"
                                    name="type"
                                    type="text"
                                    required
                                    value={transaction.type}
                                    onChange={handleChange}
                                />
                            </div>
                            <Popover>
                                <PopoverTrigger asChild>
                                    <Button
                                        variant={"outline"}
                                        className={cn(
                                            "w-[280px] justify-start text-left font-normal",
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
                            {/*<div className="grid gap-2">*/}
                            {/*    <DropdownMenu>*/}
                            {/*        <DropdownMenuTrigger asChild>*/}
                            {/*            <Label htmlFor="category">Type</Label>*/}
                            {/*            <Button name="category" variant="outline" size="icon">*/}
                            {/*                <FilterIcon*/}
                            {/*                    className="absolute h-[1.2rem] w-[1.2rem]"/>*/}
                            {/*            </Button>*/}
                            {/*        </DropdownMenuTrigger>*/}
                            {/*        <DropdownMenuContent align="end">*/}
                            {/*            {categories && categories.map((category) => (*/}
                            {/*                <DropdownMenuCheckboxItem*/}
                            {/*                    key={category}*/}
                            {/*                >*/}
                            {/*                    {category}*/}
                            {/*                </DropdownMenuCheckboxItem>*/}
                            {/*            ))}*/}
                            {/*        </DropdownMenuContent>*/}
                            {/*    </DropdownMenu>*/}
                            {/*</div>*/}

                            <Button type="submit" className="w-full" onClick={async () => {
                                if (type === "create") {
                                    const response = await transactionsApiConnector.createTransaction(token, transaction);
                                    if (response)
                                        navigate("/app");
                                } else {
                                    const response = await transactionsApiConnector.updateTransaction(token, transaction);
                                    if (response)
                                        navigate("/app");
                                }
                            }}>{action} Transaction
                            </Button>
                        </div>
                    </CardContent>
                </Card>
            </div>
        </BaseLayout>
    )

}
