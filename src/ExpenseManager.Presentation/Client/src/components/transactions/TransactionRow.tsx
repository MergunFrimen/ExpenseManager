import {TableCell, TableRow} from "@/components/ui/table.tsx";
import {Badge} from "@/components/ui/badge.tsx";
import {Button} from "@/components/ui/button"
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuGroup,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuShortcut,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import {EllipsisVerticalIcon, Pencil, Trash2} from "lucide-react"
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {ContextMenu, ContextMenuContent, ContextMenuItem, ContextMenuTrigger} from "@/components/ui/context-menu.tsx";
import {TransactionDto} from "@/components/models/TransactionDto.ts";

export function TransactionRow({transaction}: { transaction: TransactionDto }) {
    return (
        <TableRow className="items-center odd:bg-accent/20">
            <TableCell>
                <div className="font-medium">Lunch</div>
            </TableCell>
            <TableCell className="hidden sm:table-cell">Expense</TableCell>
            <TableCell className="hidden sm:table-cell">
                <Badge className="text-xs" variant="outline">
                    Food
                </Badge>
            </TableCell>
            <TableCell className="hidden md:table-cell">2023-06-23</TableCell>
            <TableCell className="hidden md:table-cell">-$10,000.00</TableCell>
            <TableCell className="text-right">
                {/*<DropdownMenuDemo/>*/}
            </TableCell>
        </TableRow>
    );
}

export function DropdownMenuDemo() {
    return (
        <Dialog>
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    <Button variant="outline">
                        <EllipsisVerticalIcon className="w-5 h-5 text-gray-500 self-center"/>
                    </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent className="w-56">
                    <DropdownMenuLabel>Actions</DropdownMenuLabel>
                    <DropdownMenuSeparator/>
                    <DropdownMenuGroup>
                        <DialogTrigger asChild>
                            <DropdownMenuItem>
                                <Pencil className="mr-2 h-4 w-4"/>
                                <span>Edit</span>
                                <DropdownMenuShortcut>⇧⌘P</DropdownMenuShortcut>
                            </DropdownMenuItem>
                        </DialogTrigger>
                        <DropdownMenuItem className="text-red-400">
                            <Trash2 className="mr-2 h-4 w-4"/>
                            <span>Delete</span>
                            <DropdownMenuShortcut>⌘B</DropdownMenuShortcut>
                        </DropdownMenuItem>
                    </DropdownMenuGroup>
                </DropdownMenuContent>
            </DropdownMenu>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Are you absolutely sure?</DialogTitle>
                    <DialogDescription>
                        This action cannot be undone. Are you sure you want to permanently
                        delete this file from our servers?
                    </DialogDescription>
                </DialogHeader>
                <DialogFooter>
                    <Button type="submit">Confirm</Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}

export function DeleteAction() {
    return <Dialog>
        <ContextMenu>
            <ContextMenuTrigger>Right click</ContextMenuTrigger>
            <ContextMenuContent>
                <ContextMenuItem>Open</ContextMenuItem>
                <ContextMenuItem>Download</ContextMenuItem>
                <DialogTrigger asChild>
                    <ContextMenuItem>
                        <span>Delete</span>
                    </ContextMenuItem>
                </DialogTrigger>
            </ContextMenuContent>
        </ContextMenu>
        <DialogContent>
            <DialogHeader>
                <DialogTitle>Are you absolutely sure?</DialogTitle>
                <DialogDescription>
                    This action cannot be undone. Are you sure you want to permanently
                    delete this file from our servers?
                </DialogDescription>
            </DialogHeader>
            <DialogFooter>
                <Button type="submit">Confirm</Button>
            </DialogFooter>
        </DialogContent>
    </Dialog>
}