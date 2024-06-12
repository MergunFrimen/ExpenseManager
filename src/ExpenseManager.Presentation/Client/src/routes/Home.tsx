import BaseLayout from "@/layouts/BaseLayout.tsx";
import {Link} from "react-router-dom";
import {Button} from "@/components/ui/button.tsx";
import {ArrowRightIcon} from "lucide-react";

export default function Home() {
    return <BaseLayout>
        <div className="container flex flex-col items-center justify-center size-full space-y-6">
            <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
                Expense Manager
            </h1>

            <p className="leading-7 [&:not(:first-child)]:mt-6">
                Expense Manager is a simple web application that helps you keep track of your expenses.
            </p>

            <div className="flex flex-col gap-y-2 w-[200px] mt-5">
                <Link to="/app" className="w-full">
                    <Button className="w-full" variant="default">
                        Go to app
                        <ArrowRightIcon className="w-5 h-5 ml-2"/>
                    </Button>
                </Link>
            </div>
        </div>
    </BaseLayout>
}