import BaseLayout from "@/layouts/BaseLayout.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";

export default function Dashboard() {
    const {token} = useAuth();

    return <BaseLayout>
        <div className="container flex flex-col size-full gap-y-3">
            {/*<TotalBalance transactions={transactions}/>*/}
            {/*<TransactionTable transactions={transactions}/>*/}
        </div>
    </BaseLayout>
}