import BaseLayout from "@/layouts/BaseLayout.tsx";
import {CategoryDonutChart} from "@/components/charts/CategoryDonutChart.tsx";
import {Balance} from "@/components/charts/Balance.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Navigate} from "react-router-dom";


export default function Stats() {
    const {token} = useAuth();

    // If the user is not authenticated, redirect to the login page
    if (!token) {
        return <Navigate to="/login"/>;
    }

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
                        <CategoryDonutChart type={"expense"}/>
                        <CategoryDonutChart type={"income"}/>
                    </div>
                    {/*<div className={"w-full pt-6"}>*/}
                    {/*    <IncomeExpenseStackedBarchart/>*/}
                    {/*</div>*/}
                </div>
            </div>
        </BaseLayout>
    )
}
