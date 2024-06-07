import {Link, useLocation, useNavigate} from "react-router-dom";
import {ModeToggle} from "@/components/theme/ModeToggle.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger
} from "@/components/ui/dropdown-menu.tsx";
import {Button} from "@/components/ui/button.tsx";
import {DownloadIcon, LogOutIcon, SettingsIcon, UploadIcon} from "lucide-react";

export function Header() {
    const location = useLocation();

    return (
        <header className="container flex justify-between items-center py-4">
            <Link to="/" className="text-xl self-start font-bold px-5">
                Expense Manager
            </Link>
            <div className="flex row gap-x-2">
                <ModeToggle/>
                {location.pathname == '/app' && <Settings/>}
            </div>
        </header>
    )
}

function Settings() {
    const navigate = useNavigate();

    return (
        <DropdownMenu>
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
                <DropdownMenuItem onClick={() => console.log("export")}>
                    <UploadIcon className="mr-2 h-4 w-4"/>
                    <span>Export</span>
                </DropdownMenuItem>
                <DropdownMenuItem onClick={() => console.log("import")}>
                    <DownloadIcon className="mr-2 h-4 w-4"/>
                    <span>Import</span>
                </DropdownMenuItem>
            </DropdownMenuContent>
        </DropdownMenu>
    )
}