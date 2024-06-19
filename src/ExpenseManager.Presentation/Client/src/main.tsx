import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {AuthProvider} from './components/auth/AuthProvider';
import {TestFormDialog} from "@/components/transactions/TestFormDialog.tsx";
import {Toaster} from "@/components/ui/toaster.tsx";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement,
);

const testTransaction: TransactionDto = {
    id: '078d2822-d80c-4d7f-beeb-6a99d5596171',
    description: 'Test',
    amount: 420,
    type: 'Expense',
    date: 1718793556,
    categoryIds: ["f4ba745e-23ca-417c-a6d2-c855d5b23cc8"],
    categoryNames: []
}

root.render(
    <React.StrictMode>
        {/*<SWRConfig value={{*/}
        {/*    refreshInterval: 100*/}
        {/*}}>*/}
        {/*<SWRDevTools>*/}
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <AuthProvider>
                {/*<Routes/>*/}
                <TestFormDialog transaction={testTransaction} type={'edit'}/>
            </AuthProvider>
        </ThemeProvider>
        {/*</SWRDevTools>*/}
        {/*</SWRConfig>*/}
        <Toaster/>
    </React.StrictMode>
)

