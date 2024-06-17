import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {toast} from "@/components/ui/use-toast.ts";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useEffect, useState} from "react";
import useSWRMutation from "swr/mutation";
import {Pencil, Trash2} from "lucide-react";
import {mutate} from "swr";
import {CategoryForm} from "./CategoryForm";

async function fetcher2(url: string, token: string | null, {arg}: { arg: { categoryId: string } }) {
    const response = await fetch(url, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            categoryIds: [arg.categoryId]
        })
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function CategoryRow({category}: { category: CategoryDto }) {
    const {token} = useAuth();
    const [open, setOpen] = useState(false);
    const [dialogType, setDialogType] = useState<'edit' | 'remove' | undefined>(undefined);

    const {
        data,
        trigger,
        error
    } = useSWRMutation(['/api/v1/categories', token], ([url, token], arg) => fetcher2(url, token, arg));

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })

    }, [error]);

    useEffect(() => {
    }, [data]);


    return (
        <div key={category.id} className={'grid grid-cols-2 items-center space-y-3'}>
            <h1>{category.name}</h1>
            <div className="flex flex-row gap-x-1 justify-end">
                <Dialog open={open} onOpenChange={() => {
                    setOpen(!open)
                }}>
                    <DialogTrigger asChild>
                        <Button variant="secondary" size="icon"
                                onClick={() => setDialogType('edit')}
                        >
                            <Pencil
                                className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogTrigger asChild>
                        <Button variant="destructive" size="icon"
                                onClick={() => setDialogType('remove')}
                        >
                            <Trash2
                                className="h-[1.2rem] w-[1.2rem]"/>
                        </Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]"
                                   onInteractOutside={(e) => e.preventDefault()}
                    >
                        {
                            dialogType === 'edit' && (
                                <>
                                    <DialogHeader>
                                        <DialogTitle>Edit</DialogTitle>
                                    </DialogHeader>
                                    <CategoryForm type={'edit'}/>
                                    <DialogFooter>
                                        {/*TODO: add form here*/}
                                        <Button
                                            type="submit"
                                        >
                                            Save changes
                                        </Button>
                                    </DialogFooter>
                                </>
                            )
                        }
                        {
                            dialogType === 'remove' && (
                                <>
                                    <DialogHeader>
                                        <DialogTitle>Delete</DialogTitle>
                                        <DialogDescription>
                                            Are you sure you want to delete this transaction? This action cannot be undone.
                                        </DialogDescription>
                                    </DialogHeader>
                                    <DialogFooter>
                                        <Button
                                            type="submit"
                                            variant="destructive"
                                            onClick={() => {
                                                trigger({categoryId: category.id})
                                                setOpen(false)
                                                mutate('/api/v1/categories')
                                            }}
                                        >
                                            Delete
                                        </Button>
                                    </DialogFooter>
                                </>
                            )
                        }
                    </DialogContent>
                </Dialog>
            </div>
        </div>
    )
}

