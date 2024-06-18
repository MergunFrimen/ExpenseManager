import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {Dialog, DialogContent, DialogTrigger} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useState} from "react";
import {Pencil, Trash2} from "lucide-react";
import {EditCategoryDialog} from "@/components/categories/EditCategoryDialog.tsx";
import {RemoveCategoryDialog} from "@/components/categories/RemoveCategoryDialog.tsx";
import {CategoryForm} from "@/components/categories/CategoryForm.tsx";

export function CategoryRow({category}: { category: CategoryDto }) {
    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState<'edit' | 'remove' | undefined>(undefined);

    return (
        <div key={category.id} className={'grid grid-cols-2 items-center space-y-3'}>
            <h1>{category.name}</h1>
            <div className="flex flex-row gap-x-1 justify-end">
                <Dialog open={open} onOpenChange={setOpen}>
                    <DialogTrigger asChild>
                        <Button variant="secondary" size="icon"
                                onClick={() => setDialogType('edit')}>
                            <Pencil className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogTrigger asChild>
                        <Button variant="destructive" size="icon"
                                onClick={() => setDialogType('remove')}>
                            <Trash2 className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                        <CategoryDialogContext category={category} type={dialogType} setOpen={setOpen}/>
                    </DialogContent>
                </Dialog>
            </div>
        </div>
    )
}

function CategoryDialogContext({type, category, setOpen}: {
    type: 'edit' | 'remove' | undefined,
    category: CategoryDto,
    setOpen: (open: boolean) => void
}) {
    if (type === 'edit') {
        return <CategoryForm type={'edit'} category={category} setOpen={setOpen}/>
    }
    if (type === 'remove') {
        return <RemoveCategoryDialog category={category} setOpen={setOpen}/>
    }
    return <></>
}
