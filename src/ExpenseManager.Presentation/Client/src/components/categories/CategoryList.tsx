import useSWRMutation from "swr/mutation";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "@/components/ui/use-toast.ts";
import {useEffect, useState} from "react";
import {PlusIcon, RefreshCwIcon} from "lucide-react";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {CategoryRow} from "@/components/categories/CategoryRow.tsx";
import {Button} from "@/components/ui/button.tsx";
import {CategoryFilterDialog} from "@/components/categories/CategoryFilterDialog.tsx";
import {Dialog, DialogContent, DialogTrigger} from "@/components/ui/dialog.tsx";
import {CreateCategoryForm} from "@/components/categories/CreateCategoryForm.tsx";
import {TabsList, TabsTrigger} from "@/components/ui/tabs.tsx";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";

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

export function CategoryList() {
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
    const [open, setOpen] = useState(false);

    function onSubmit(e: { name?: string }) {
        trigger({filters: e} as any);
    }

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

    useEffect(() => {
        trigger({filters: {}} as any);
    }, []);

    return (
        <div className="flex flex-col gap-y-3">
            <div className="flex flex-row gap-x-3 justify-between">
                <TabsList>
                    <TabsTrigger value="transactions">Transactions</TabsTrigger>
                    <TabsTrigger value="categories">Categories</TabsTrigger>
                </TabsList>

                <div className="flex flex-row gap-x-3">
                    <Dialog open={open} onOpenChange={setOpen}>
                        <DialogTrigger asChild>
                            <Button
                                variant="default"
                                className="bg-green-500"
                            >
                                <PlusIcon className="h-[1.2rem] w-[1.2rem]"/>
                                Add new
                            </Button>
                        </DialogTrigger>
                        <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                            <CreateCategoryForm setOpen={setOpen}/>
                        </DialogContent>
                    </Dialog>
                    <CategoryFilterDialog form={form} onSubmit={onSubmit}/>
                    <Button variant="ghost" size="icon" onClick={
                        () => trigger({filters: {}} as any)
                    }>
                        <RefreshCwIcon className="h-[1.2rem] w-[1.2rem]"/>
                    </Button>
                </div>
            </div>
            <ScrollArea className={'size-full h-[600px] outline outline-1 outline-accent rounded-md px-5'}>
                {data && data.sort((x: CategoryDto, y: CategoryDto) => x.id > y.id ? 1 : -1).map((category: CategoryDto) =>
                    <CategoryRow key={category.id} category={category}/>
                )}
            </ScrollArea>
        </div>
    );
}

