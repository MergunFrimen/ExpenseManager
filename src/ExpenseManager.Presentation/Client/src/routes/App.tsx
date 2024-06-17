import BaseLayout from "@/layouts/BaseLayout.tsx";
import {TotalBalance} from "@/components/transactions/TotalBalance";
import {Tabs, TabsContent, TabsList, TabsTrigger} from "@/components/ui/tabs.tsx";
import Categories from "@/routes/Categories.tsx";

export default function Dashboard() {

    return (<BaseLayout>
            <div className="container flex flex-col items-center size-full space-y-6">
                <TotalBalance/>
                <Tabs defaultValue="categories" className={"w-full"}>
                    <TabsList>
                        <TabsTrigger value="transactions">Transactions</TabsTrigger>
                        <TabsTrigger value="categories">Categories</TabsTrigger>
                    </TabsList>
                    <TabsContent value="transactions">Make changes to your account here.</TabsContent>
                    <TabsContent value="categories">
                        <Categories/>
                    </TabsContent>
                </Tabs>
                {/*<Categories/>*/}

                {/*<TransactionTable transactions={transactions}/>*/}
            </div>
        </BaseLayout>
    )
}