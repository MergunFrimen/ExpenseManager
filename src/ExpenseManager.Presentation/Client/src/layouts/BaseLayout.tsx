import {ReactNode} from "react";
import {ThemeProvider} from "@/components/theme/ThemeProvider.tsx";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
            <ModeToggle />
            <div className="size-full">
                <h1>BaseLayout</h1>
                {children}
            </div>
        </ThemeProvider>
    )
}