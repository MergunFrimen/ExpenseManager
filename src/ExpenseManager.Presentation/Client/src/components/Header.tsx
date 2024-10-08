import {useNavigate} from "react-router-dom";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSeparator,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {BarChartBig, LayoutGridIcon, ListIcon, LogOutIcon, SettingsIcon} from "lucide-react";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Export} from "@/components/export/Export.tsx";
import {Import} from "@/components/import/Import.tsx";

export function Header() {
    return (
        <header
            className="container z-[1] left-0 right-0 top-0 bg-background fixed flex justify-between items-center py-4">
            <div className="flex row gap-x-2">
                <Navigation/>
            </div>
            <div className="flex row gap-x-2">
                <ModeToggle/>
                <Settings/>
            </div>
        </header>
    )
}

function Navigation() {
    const {token} = useAuth();
    const navigate = useNavigate();

    if (!token)
        return <></>

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon">
                    <ListIcon
                        className="absolute h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
                <DropdownMenuItem onClick={() => navigate("/")}>
                    <LayoutGridIcon className="mr-2 h-4 w-4"/>
                    <span>Dashboard</span>
                </DropdownMenuItem>
                <DropdownMenuItem onClick={() => navigate("/stats")}>
                    <BarChartBig className="mr-2 h-4 w-4"/>
                    <span>Statistics</span>
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    )
}

function Settings() {
    const {token} = useAuth();
    const navigate = useNavigate();

    if (!token)
        return <></>

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon">
                    <SettingsIcon
                        className="absolute h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
                <DropdownMenuItem onClick={() => navigate("/logout")}>
                    <LogOutIcon className="mr-2 h-4 w-4"/>
                    <span>Logout</span>
                </DropdownMenuItem>
                <DropdownMenuSeparator/>
                <Export/>
                <Import/>
            </DropdownMenuContent>
        </DropdownMenu>
    )
}