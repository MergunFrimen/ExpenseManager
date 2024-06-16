import {ReactNode} from "react";
import {Header} from "@/components/Header.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <div className="flex flex-col p-2">
            <Header/>
            {children}
        </div>
    )
}