import BaseLayout from "@/layouts/BaseLayout.tsx";
import {Link} from "react-router-dom";
import {Button} from "@/components/ui/button.tsx";

export default function RootPage() {
    return <BaseLayout>
        <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
            Expense Manager
        </h1>

        <p className="leading-7 [&:not(:first-child)]:mt-6">
            Expense Manager is a simple web application that helps you keep track of your expenses.
        </p>

        <div className="flex flex-col gap-y-2 w-[200px] mt-5">
            <Link to="/auth/register" className="w-full">
                <Button className="w-full" variant="default">
                    Login
                </Button>
            </Link>
            <Link to="/auth/register" className="w-full">
                <Button className="w-full" variant="secondary">
                    Sign up
                </Button>
            </Link>
        </div>
    </BaseLayout>
}