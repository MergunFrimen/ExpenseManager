import useSWR from 'swr';
import {BalanceDto} from "@/models/charts/Balance.ts";

const fetcher = (url: string) => fetch(url, {
    method: "GET",
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0NzlmMjkzZi1iY2Y3LTRmMDgtOThkOC1jNjk3Y2FkZTZkM2UiLCJnaXZlbl9uYW1lIjoiRG9taW5payIsImZhbWlseV9uYW1lIjoiVGljaHkiLCJqdGkiOiJjNWVjMWJiMi0wZTg1LTQxZDMtODI5Yy0zZTZkZGQ1MzBjM2IiLCJpc3MiOiJFeHBlbnNlTWFuYWdlciIsImF1ZCI6IkV4cGVuc2VNYW5hZ2VyIiwiZXhwIjoxNzE4ODQ0MzMzfQ.KX20Dz1q1ghopNcZT0_EeH1i35wl1-zuw9u5tv1r32s"
    },
}).then((res) => res.json());

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#F2545B']

export function Balance() {
    const {data, error, isLoading} = useSWR<BalanceDto>(
        "/api/v1/statistics/balance",
        fetcher
    );

    if (isLoading) return <div>Loading...</div>;
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
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#0088FE]"}>{totalBalance} CZK</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total income:</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#00C49F]"}>{totalIncome} CZK</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-[]"}>Total expenses:</span>
            <span className={"scroll-m-20 text-xl font-semibold tracking-tight text-right text-[#F2545B]"}>{totalExpenses} CZK</span>
        </div>
    )
}