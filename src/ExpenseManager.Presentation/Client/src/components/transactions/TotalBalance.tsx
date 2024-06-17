import {useAuth} from "@/components/auth/AuthProvider.tsx";
import useSWR from "swr";

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
    const {data, error, isLoading} = useSWR(
        ["/api/v1/statistics/balance", token],
        ([url, token]) => fetcher(url, token)
    );

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error loading data</div>;

    if (!data) return <div>No data</div>;

    function numberWithCommas(number: number) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    const totalBalance = numberWithCommas(data['totalBalance']);

    return (
        <div className={"grid grid-cols-2 w-[280px]"}>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total balance:</span>
            <span
                className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#0088FE]"}>{totalBalance} CZK</span>
        </div>
    )
}