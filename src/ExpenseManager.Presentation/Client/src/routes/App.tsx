import BaseLayout from "@/layouts/BaseLayout.tsx";
import {TotalBalance} from "@/components/transactions/TotalBalance";
import {Tabs, TabsContent, TabsList, TabsTrigger} from "@/components/ui/tabs.tsx";
import CategoryList from "@/components/categories/CategoryList.tsx";

export default function App() {

    return (<BaseLayout>
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

                <Tabs defaultValue="categories" className={"w-full"}>
                    <TabsList>
                        <TabsTrigger value="transactions">Transactions</TabsTrigger>
                        <TabsTrigger value="categories">Categories</TabsTrigger>
                    </TabsList>
                    <TabsContent value="transactions">Make changes to your account here.</TabsContent>
                    <TabsContent value="categories">
                        <CategoryList/>
                    </TabsContent>
                </Tabs>
                {/*<CategoryList/>*/}

                {/*<TransactionTable transactions={transactions}/>*/}
            </div>
        </BaseLayout>
    )
}