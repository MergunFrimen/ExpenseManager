import ReactDOM from 'react-dom/client'
import React from "react";
import './index.css'
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {AuthProvider} from './components/auth/AuthProvider';
import {Routes} from "@/routes/Routes.tsx";
import {SWRDevTools} from "swr-devtools";
import {SWRConfig} from "swr";

const root = ReactDOM.createRoot(
    document.getElementById("root") as HTMLElement,
);

root.render(
    <React.StrictMode>
        <SWRConfig value={{
            // refreshInterval: 100
        }}>
            <SWRDevTools>
            <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                <AuthProvider>
                    <Routes/>
                </AuthProvider>
            </ThemeProvider>
            </SWRDevTools>
        </SWRConfig>
    </React.StrictMode>
)

