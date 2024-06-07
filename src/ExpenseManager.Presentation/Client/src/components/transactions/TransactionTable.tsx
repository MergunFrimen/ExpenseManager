import {Card, CardContent, CardHeader, CardTitle,} from "@/components/ui/card"
import {TableBody, TableFixed, TableHead, TableHeaderFixed} from "@/components/ui/table";
import {TransactionRow} from "@/components/transactions/TransactionRow.tsx";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useEffect, useState} from "react";
import {DownloadIcon, FilterIcon, LogOutIcon, SettingsIcon, UploadIcon} from "lucide-react";
import {Button} from "@/components/ui/button.tsx";
import {
    DropdownMenu, DropdownMenuCheckboxItem,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {categoriesApiConnector} from "@/api/categoriesApiConnector.ts";

export default function TransactionTable({transactions}: { transactions: TransactionDto[] }) {
    const {token} = useAuth();
    const [categories, setCategories] = useState([])
    const [filteredTransactions, setFilteredTransactions] = useState(transactions);

    function handleFilter(id: string) {
        const categoriesStatus = categories.map((category) => {
                return {
                    id: category.id,
                    name: category.name,
                    checked: category.id === id ? !category.checked : category.checked
                }
            }
        );
        setCategories(categoriesStatus);
    }

    useEffect(() => {
        const filteredTransactions = transactions.filter((transaction) => {
            return categories.find((category) => category.id === transaction.categoryId && category.checked);
        });

        setFilteredTransactions(filteredTransactions);
    }, [categories]);


    useEffect(() => {
        async function fetchCategories() {
            if (!token) return;

            const categories = await categoriesApiConnector.getCategories(token);
            const categoriesStatus = categories.map((category) => {
                    return {
                        id: category.id,
                        name: category.name,
                        checked: true
                    }
                }
            );


            setCategories(categoriesStatus);
        }

        fetchCategories();
    }, [token]);

    return (
        <Card className="p-5 h-[700px]">
            <CardHeader className="px-7 flex flex-row justify-between">
                <CardTitle>Transactions</CardTitle>
                <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                        <Button variant="outline" size="icon">
                            <FilterIcon
                                className="absolute h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                        {categories && categories.map((category) => (
                            <DropdownMenuCheckboxItem
                                key={category.id}
                                checked={category.checked}
                                onClick={() => handleFilter(category.id)}
                            >
                                {category.name}
                            </DropdownMenuCheckboxItem>
                        ))}
                    </DropdownMenuContent>
                </DropdownMenu>
            </CardHeader>
            <CardContent className="h-[580px]">
                <ScrollArea className="size-full rounded-md border">
                    <TableFixed className="size-full">
                        <TableHeaderFixed className="z-40">
                            <TableHead>Description</TableHead>
                            <TableHead className="hidden sm:table-cell">Type</TableHead>
                            <TableHead className="hidden sm:table-cell">Category</TableHead>
                            <TableHead className="hidden md:table-cell">Date</TableHead>
                            <TableHead className="hidden md:table-cell">Amount</TableHead>
                            <TableHead className="text-right"></TableHead>
                        </TableHeaderFixed>
                        <TableBody className="">
                            {filteredTransactions && filteredTransactions.map((transaction) => (
                                <TransactionRow key={transaction.id} transaction={transaction}/>
                            ))}
                        </TableBody>
                    </TableFixed>
                </ScrollArea>
            </CardContent>
        </Card>
    )
}
