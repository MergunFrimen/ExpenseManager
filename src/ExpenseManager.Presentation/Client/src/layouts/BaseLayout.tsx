import {ReactNode} from "react";
import {Header} from "@/components/Header.tsx";
import {Toaster} from "@/components/ui/toaster.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <div className="flex flex-col pb-2 size-full">
            <Header/>
            <main className={'mt-[80px] size-full'}>
                {children}
            </main>
            <Toaster/>
        </div>
    )
}