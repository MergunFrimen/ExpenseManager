import {Link} from "react-router-dom";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";

export function Header() {
    return (
        <header className="shadow-sm">
            <div className="container flex justify-between items-center py-4">
                <Link to="/" className="text-xl self-start font-bold">
                    Expense Manager
                </Link>
                {/*<nav>*/}
                {/*    <ul className="flex gap-4">*/}
                {/*        <li>*/}
                {/*            <Link to="/auth/login">Login</Link>*/}
                {/*        </li>*/}
                {/*        <li>*/}
                {/*            <Link to="/auth/register">Register</Link>*/}
                {/*        </li>*/}
                {/*    </ul>*/}
                {/*</nav>*/}
                <ModeToggle />
            </div>
        </header>
    )
}