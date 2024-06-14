import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {AuthProvider} from './components/auth/AuthProvider';
import {TransactionForm} from "@/components/transactions/TransactionForm.tsx";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement,
);

root.render(
    <React.StrictMode>
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <AuthProvider>
                {/*<Routes/>*/}
                <TransactionForm type={"create"}/>
            </AuthProvider>
        </ThemeProvider>
    </React.StrictMode>
)

