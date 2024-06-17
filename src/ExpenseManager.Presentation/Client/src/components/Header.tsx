import {useLocation, useNavigate} from "react-router-dom";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem, DropdownMenuSeparator,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {DownloadIcon, LogOutIcon, SettingsIcon, UploadIcon} from "lucide-react";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Export} from "@/components/export/Export.tsx";
import {Import} from "@/components/import/Import.tsx";

export function Header() {
    return (
        <header
            className="container z-[1] left-0 right-0 top-0 bg-background fixed flex justify-between items-center py-4">
            <div to="/" className="text-xl self-start font-bold px-5">
                Expense Manager
            </div>
            <div className="flex row gap-x-2">
                <ModeToggle/>
                <Settings/>
            </div>
        </header>
    )
}

function Settings() {
    const navigate = useNavigate();

    return (
        <DropdownMenu open={true}>
            <DropdownMenuTrigger asChild>
                <Button variant="outline" size="icon">
                    <SettingsIcon
                        className="absolute h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
                <DropdownMenuItem onClick={() => navigate("/app/logout")}>
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