import BaseLayout from "@/layouts/BaseLayout.tsx";
import useSWRMutation from "swr/mutation";
import {Button} from "@/components/ui/button";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {Form, FormControl, FormField, FormItem, FormMessage} from "@/components/ui/form";
import {toast, useToast} from "@/components/ui/use-toast.ts";
import {useEffect, useState} from "react";
import {CircleXIcon, Pencil, SearchIcon, Trash2} from "lucide-react";
import {Input} from "@/components/ui/input";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {mutate} from "swr";
import {useAuth} from "@/components/auth/AuthProvider.tsx";

const formSchema = z.object({
    name: z.string()
})

async function fetcher(url: string, token: string | null, {arg}: { arg: { filters: { name?: string } } }) {
    const response = await fetch(`${url}/search`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify(arg)
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

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

export default function Categories() {
    const {token} = useAuth();
    const {toast} = useToast()
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: ''
        },
    })
    const {
        data,
        trigger,
        error
    } = useSWRMutation(['/api/v1/categories', token], ([url, token], arg) => fetcher(url, token, arg));


    function onSubmit(e: { name?: string }) {
        trigger({filters: e});
    }

    // useEffect(() => {
    //     if (!data)
    //         trigger({filters: {}});
    // }, [data]);


    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

    return (
        <div className="flex flex-col gap-y-3 p-2">
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
                    <FormField
                        control={form.control}
                        name="name"
                        render={({field}) => (
                            <FormItem>
                                {/*<FormLabel>Category name</FormLabel>*/}
                                <FormControl>
                                    <div className="relative w-full">
                                        <div
                                            className="absolute inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
                                            <SearchIcon className="w-4 h-4 text-gray-500 dark:text-gray-400"/>
                                        </div>
                                        <button type={'button'}
                                                className="absolute inset-y-0 end-0 flex items-center pe-3"
                                                onClick={() => {
                                                    form.reset({name: ""})
                                                    trigger({filters: {}})
                                                }}
                                        >
                                            <CircleXIcon className="w-4 h-4 text-gray-500 dark:text-gray-400"/>
                                        </button>
                                        <Input
                                            placeholder="Search category"
                                            className="h-10 block w-full p-4 ps-10"
                                            {...field}
                                        />
                                    </div>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                </form>
            </Form>
            <ScrollArea className={'size-full h-[400px] outline outline-1 outline-accent rounded-md p-5'}>
                {data && data.map(category =>
                    <CategoryRow key={category.id} category={category}/>
                )}
            </ScrollArea>
        </div>)
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
                    <DialogContent className="sm:max-w-[425px]">
                        {
                            dialogType === 'edit' && (
                                <>
                                    <DialogHeader>
                                        <DialogTitle>Edit</DialogTitle>
                                        <DialogDescription>
                                            Edit the category name and click save to update the category.
                                        </DialogDescription>
                                    </DialogHeader>
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
        ;
}
