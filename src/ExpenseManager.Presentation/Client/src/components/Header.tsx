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
import {AreaChartIcon, HomeIcon, LayoutGridIcon, ListIcon, LogOutIcon, SettingsIcon} from "lucide-react";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Export} from "@/components/export/Export.tsx";
import {Import} from "@/components/import/Import.tsx";

export function Header() {
    const {token} = useAuth();

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
                    <DropdownMenuItem>
                        <Link to="/" className="flex items-center size-full">
                            <HomeIcon className="mr-2 h-4 w-4"/>
                            <span>Home page</span>
                        </Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem>
                        <Link to="/app" className="flex items-center size-full">
                            <LayoutGridIcon className="mr-2 h-4 w-4"/>
                            <span>App page</span>
                        </Link>
                    </DropdownMenuItem>
                    <DropdownMenuItem>
                        <Link to="/app/stats" className="flex items-center size-full">
                            <AreaChartIcon className="mr-2 h-4 w-4"/>
                            <span>Stats page</span>
                        </Link>
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