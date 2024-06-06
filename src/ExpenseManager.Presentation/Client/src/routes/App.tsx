import BaseLayout from "@/layouts/BaseLayout.tsx";
import TransactionTable from "@/components/transactions/TransactionTable.tsx";
import {TotalBalance} from "@/components/transactions/TotalBalance.tsx";

export default function App() {
    return <BaseLayout>
        <div className="flex flex-col size-full gap-y-3">
            <TotalBalance/>
            <TransactionTable/>
        </div>
    </BaseLayout>
}