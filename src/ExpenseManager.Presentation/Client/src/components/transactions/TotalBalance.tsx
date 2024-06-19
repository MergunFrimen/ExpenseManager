import {useAuth} from "@/components/auth/AuthProvider.tsx";
import useSWR from "swr";
import {Skeleton} from "../ui/skeleton";

async function fetcher(url: string, token: string | null) {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

// const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#F2545B']

export function TotalBalance() {
    const {token} = useAuth();
    const {data, isLoading} = useSWR(
        ["/api/v1/statistics/balance", token],
        ([url, token]) => fetcher(url, token),
        {
            refreshInterval: 1000
        }
    );

    if (isLoading)
        return (
            <Skeleton className={"grid grid-cols-2 grid-rows-3 w-[280px] h-[28px]"}>
                <Skeleton className={"h-[28px] w-full"}/>
            </Skeleton>
        )

    function numberWithCommas(number: number) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    const totalBalance = numberWithCommas(data['totalBalance']);

    return (
        <div className={"grid grid-cols-2 w-[280px]"}>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total balance:</span>
            <span
                className={"scroll-m-20 text-xl font-semibold tracking-tight text-right"}>{totalBalance} CZK</span>
        </div>
    )
}