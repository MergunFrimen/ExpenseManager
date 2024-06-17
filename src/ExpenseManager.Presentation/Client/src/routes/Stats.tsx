import BaseLayout from "@/layouts/BaseLayout.tsx";
import {CategoryDonutChart} from "@/components/charts/CategoryDonutChart.tsx";
import useSWR from "swr";
import {Balance} from "@/components/charts/Balance.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";

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

export default function Stats() {
    const {token} = useAuth();
    const {data, error, isLoading} = useSWR(
        ["/api/v1/statistics/charts", token],
        ([url, token]) => fetcher(url, token)
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