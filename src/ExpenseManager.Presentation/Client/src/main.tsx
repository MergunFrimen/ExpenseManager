import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {AuthProvider} from './components/auth/AuthProvider';
import {Toaster} from "@/components/ui/toaster.tsx";
import {Routes} from "@/routes/Routes.tsx";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement,
);

root.render(
    <React.StrictMode>
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <AuthProvider>
                <Routes/>
            </AuthProvider>
        </ThemeProvider>
        <Toaster/>
    </React.StrictMode>
)

