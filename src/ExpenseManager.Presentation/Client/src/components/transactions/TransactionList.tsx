import useSWRMutation from "swr/mutation";
import {useEffect} from "react";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {CategoryRow} from "@/components/categories/CategoryRow.tsx";
import { TransactionRow } from "./TransactionRow";

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
    const {
        data,
        trigger,
    } = useSWRMutation(['/api/v1/transactions', token], ([url, token], arg) => fetcher(url, token, arg));

    useEffect(() => {
        trigger({
                filters: {
                    priceRange: {},
                    dateRange: {},
                    // transactionType: "Income",
                    categories: []
                }
            }
        );
    }, []);

    return (
        <div className="flex flex-col gap-y-3 p-2">
            <ScrollArea className={'size-full h-[400px] outline outline-1 outline-accent rounded-md p-5'}>
                {data && data.map(transaction =>
                    <TransactionRow key={transaction.id} transaction={transaction}/>
                )}
            </ScrollArea>
        </div>
    );
}

