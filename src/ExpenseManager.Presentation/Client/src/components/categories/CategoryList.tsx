import useSWRMutation from "swr/mutation";
import {z} from "zod";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {Form, FormControl, FormField, FormItem, FormMessage} from "@/components/ui/form";
import {useToast} from "@/components/ui/use-toast.ts";
import {useEffect} from "react";
import {CircleXIcon, SearchIcon} from "lucide-react";
import {Input} from "@/components/ui/input";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {CategoryRow} from "@/components/categories/CategoryRow.tsx";

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


    function onSubmit(e: { name?: string }) {
        trigger({filters: e});
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
        trigger({filters: {}});
    }, []);

    return (
        <div className="flex flex-col gap-y-3 p-2">
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}
                      className="space-y-8 flex flex-row size-full items-center justify-center">
                    <FormField
                        control={form.control}
                        name="name"
                        render={({field}) => (
                            <FormItem className="size-full">
                                {/*<FormLabel>Category name</FormLabel>*/}
                                <FormControl>
                                    <div className="relative">
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
        </div>
    );
}

