import {ReactNode} from "react";
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {Header} from "@/components/Header.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <div className="flex h-screen w-screen flex-col p-2">
                <Header/>
                {children}
            </div>
        </ThemeProvider>
    )
}