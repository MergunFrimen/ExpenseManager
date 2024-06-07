import {ReactNode} from "react";
import {Header} from "@/components/Header.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
            <div className="flex h-screen w-screen flex-col p-2">
                <Header/>
                {children}
            </div>
    )
}