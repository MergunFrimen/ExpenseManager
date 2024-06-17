import {Link, useNavigate} from "react-router-dom";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSeparator,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {
    AreaChartIcon,
    HomeIcon,
    LayoutGridIcon,
    ListIcon,
    LogInIcon,
    LogOutIcon,
    NotebookPenIcon,
    SettingsIcon
} from "lucide-react";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Export} from "@/components/export/Export.tsx";
import {Import} from "@/components/import/Import.tsx";

export function Header() {
    const {token} = useAuth();
    const navigate = useNavigate();

    return (
        <header
            className="container z-[1] left-0 right-0 top-0 bg-background fixed flex justify-between items-center py-4">
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    <Button variant="outline" size="icon">
                        <ListIcon
                            className="absolute h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                    <DropdownMenuItem onClick={() => navigate("/")}>
                        <HomeIcon className="mr-2 h-4 w-4"/>
                        <span>Home page</span>
                    </DropdownMenuItem>
                    <DropdownMenuItem onClick={() => navigate("/login")}>
                        <LogInIcon className="mr-2 h-4 w-4"/>
                        <span>Login</span>
                    </DropdownMenuItem>
                    <DropdownMenuItem onClick={() => navigate("/register")}>
                        <NotebookPenIcon className="mr-2 h-4 w-4"/>
                        <span>Register</span>
                    </DropdownMenuItem>
                    <DropdownMenuSeparator/>
                    <DropdownMenuItem onClick={() => navigate("/app")}>
                        <LayoutGridIcon className="mr-2 h-4 w-4"/>
                        <span>App page</span>
                    </DropdownMenuItem>
                    <DropdownMenuItem onClick={() => navigate("/app/stats")}>
                        <AreaChartIcon className="mr-2 h-4 w-4"/>
                        <span>Stats page</span>
                    </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>

            {/*<Link to="/" className="text-xl self-start font-bold px-5">*/}
            {/*    Expense Manager*/}
            {/*</Link>*/}
            <div className="flex row gap-x-2">
                <ModeToggle/>
                {token && <Settings/>}
            </div>
        </header>
    )
}

function Settings() {
    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon">
                    <SettingsIcon
                        className="absolute h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
                <DropdownMenuItem>
                    <Link to="/app/logout" className="flex items-center size-full">
                        <LogOutIcon className="mr-2 h-4 w-4"/>
                        <span>Logout</span>
                    </Link>
                </DropdownMenuItem>
                <DropdownMenuSeparator/>
                <div className={'flex flex-col wrap gap-y-2'}>
                    <Export/>
                    <Import/>
                </div>
            </DropdownMenuContent>
        </DropdownMenu>
    )
}