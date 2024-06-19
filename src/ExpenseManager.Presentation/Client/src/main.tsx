import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {AuthProvider} from './components/auth/AuthProvider';
import {Toaster} from "@/components/ui/toaster.tsx";
import {TransactionFilterFormDialog} from "@/components/transactions/TransactionFilterFormDialog.tsx";
import {Button} from "@/components/ui/button.tsx";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement,
);

root.render(
    <React.StrictMode>
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <AuthProvider>
                {/*<Routes/>*/}
                <TransactionFilterFormDialog>
                    <Button>
                        Press
                    </Button>
                </TransactionFilterFormDialog>
            </AuthProvider>
        </ThemeProvider>
        <Toaster/>
    </React.StrictMode>
)

