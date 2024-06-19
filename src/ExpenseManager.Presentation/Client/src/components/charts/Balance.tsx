import useSWR from 'swr';
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Skeleton} from "@/components/ui/skeleton.tsx";

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

export function Balance() {
    const {token} = useAuth();
    const {data, error, isLoading} = useSWR(
        ["/api/v1/statistics/balance", token],
        ([url, token]) => fetcher(url, token),
        {
            // refreshInterval: 100
        }
    );

    if (isLoading)
        return (
            <Skeleton className={"grid grid-cols-2 grid-rows-3 w-[280px]"}>
                <Skeleton className={"h-[20px] w-[100px]"}/>
                <Skeleton className={"h-[20px] w-[100px]"}/>
                <Skeleton className={"h-[20px] w-[100px]"}/>
                <Skeleton className={"h-[20px] w-[100px]"}/>
                <Skeleton className={"h-[20px] w-[100px]"}/>
                <Skeleton className={"h-[20px] w-[100px]"}/>
            </Skeleton>
        )

    if (error) return <div>Error loading data</div>;

    if (!data) return <div>No data</div>;

    function numberWithCommas(number: number) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    const totalBalance = numberWithCommas(data['totalBalance']);
    const totalExpenses = numberWithCommas(data['totalExpenses']);
    const totalIncome = numberWithCommas(data['totalIncome']);

    return (
        <div className={"grid grid-cols-2 grid-rows-3 w-[280px]"}>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total balance:</span>
            <span
                className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#0088FE]"}>{totalBalance} CZK</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total income:</span>
            <span
                className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#00C49F]"}>{totalIncome} CZK</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total expenses:</span>
            <span
                className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#F2545B]"}>{totalExpenses} CZK</span>
        </div>
    )
}