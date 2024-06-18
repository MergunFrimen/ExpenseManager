import {DialogFooter, DialogHeader, DialogTitle} from "@/components/ui/dialog.tsx";
import {CategoryForm} from "@/components/categories/CategoryForm.tsx";
import {Button} from "@/components/ui/button.tsx";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";

export function EditCategoryDialog({category, setOpen}: { category: CategoryDto, setOpen: (open: boolean) => void }) {
    return <>
        <CategoryForm type={'edit'} category={category} setOpen={setOpen}/>
    </>
}