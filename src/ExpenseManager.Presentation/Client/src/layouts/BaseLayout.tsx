import {ReactNode} from "react";
import {Header} from "@/components/Header.tsx";

export default function BaseLayout({children}: { children: ReactNode }) {
    return (
        <div className="flex flex-col pb-2">
            <Header/>
            <div className={'mt-[80px] size-full'}>
                {children}
            </div>
        </div>
    )
}