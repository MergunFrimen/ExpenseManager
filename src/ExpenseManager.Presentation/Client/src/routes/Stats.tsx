import BaseLayout from "@/layouts/BaseLayout.tsx";
import {CategoryDonutChart} from "@/components/charts/CategoryDonutChart.tsx";
import useSWR from "swr";
import {Balance} from "@/components/charts/Balance.tsx";
import IncomeExpenseStackedBarchart from "@/components/charts/IncomeExpenseStackedBarchart.tsx";

const fetcher = (url: string) => fetch(url, {
    method: "GET",
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0NzlmMjkzZi1iY2Y3LTRmMDgtOThkOC1jNjk3Y2FkZTZkM2UiLCJnaXZlbl9uYW1lIjoiRG9taW5payIsImZhbWlseV9uYW1lIjoiVGljaHkiLCJqdGkiOiJjNWVjMWJiMi0wZTg1LTQxZDMtODI5Yy0zZTZkZGQ1MzBjM2IiLCJpc3MiOiJFeHBlbnNlTWFuYWdlciIsImF1ZCI6IkV4cGVuc2VNYW5hZ2VyIiwiZXhwIjoxNzE4ODQ0MzMzfQ.KX20Dz1q1ghopNcZT0_EeH1i35wl1-zuw9u5tv1r32s"
    },
}).then((res) => res.json());

export default function Stats() {
    const {data, error, isLoading} = useSWR(
        "/api/v1/statistics/charts",
        fetcher
    );

    if (isLoading) return <div>Loading...</div>;
    if (error) return <div>Error loading data</div>;

    return (
        <BaseLayout>
            <div className="container flex flex-col items-center size-full space-y-6">
                <div className={"flex flex-col items-center"}>
                    <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
                        Statistics
                    </h1>
                    <p className="text-lg text-gray-500">
                        Here you can see your income and expenses in a visual way
                    </p>
                    <div className={"w-full pt-10"}>
                        <Balance/>
                    </div>
                    <div className={"flex justify-center gap-6 flex-row grow pt-10 flex-wrap"}>
                        <CategoryDonutChart type={"expense"} data={data}/>
                        <CategoryDonutChart type={"income"} data={data}/>
                    </div>
                    {/*<div className={"w-full pt-6"}>*/}
                    {/*    <IncomeExpenseStackedBarchart/>*/}
                    {/*</div>*/}
                </div>
            </div>
        </BaseLayout>
    )
}