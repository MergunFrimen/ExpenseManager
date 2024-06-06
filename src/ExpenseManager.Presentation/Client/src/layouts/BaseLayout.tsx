import {ReactNode} from "react";
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {Header} from "@/components/Header.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <Header/>
            <div className="container">
                {children}
            </div>
        </ThemeProvider>
    )
}