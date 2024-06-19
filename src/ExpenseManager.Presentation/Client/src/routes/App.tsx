import BaseLayout from "@/layouts/BaseLayout.tsx";
import {TotalBalance} from "@/components/transactions/TotalBalance";
import {Tabs, TabsContent} from "@/components/ui/tabs.tsx";
import {CategoryList} from "@/components/categories/CategoryList.tsx";
import {TransactionList} from "@/components/transactions/TransactionList.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Navigate} from "react-router-dom";

export default function App() {
    const {token} = useAuth();

    // If the user is not authenticated, redirect to the login page
    if (!token) {
        return <Navigate to="/login"/>;
    }

    return (
        <BaseLayout>
            <div className="container flex flex-col items-center size-full space-y-6">
                <div className={"flex flex-col items-center w-full"}>
                    <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
                        Dashboard
                    </h1>
                    <p className="text-lg text-gray-500">
                        Here you can manage your transactions and categories
                    </p>
                </div>

                <div className={"flex flex-col items-start w-full"}>
                    <TotalBalance/>
                </div>

                <Tabs defaultValue="transactions" className={"w-full"}>
                    <TabsContent value="transactions">
                        <TransactionList/>
                    </TabsContent>
                    <TabsContent value="categories">
                        <CategoryList/>
                    </TabsContent>
                </Tabs>
            </div>
        </BaseLayout>
    )
}