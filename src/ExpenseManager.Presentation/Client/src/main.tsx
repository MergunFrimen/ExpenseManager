import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {Routes} from "@/routes/Routes.tsx";
import {AuthProvider} from "@/components/auth/AuthProvider.tsx";
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";

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
    </React.StrictMode>
)

