import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Form, FormControl, FormField, FormItem, FormMessage} from "@/components/ui/form.tsx";
import {Input} from "@/components/ui/input.tsx";
import {ScrollArea} from "@/components/ui/scroll-area.tsx";
import {CategoryRow} from "@/components/categories/CategoryRow.tsx";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "../ui/use-toast";
import useSWRMutation from "swr/mutation";
import {useEffect} from "react";
import {CircleXIcon, SearchIcon} from "lucide-react";
import {z} from "zod";
import {Label} from "@/components/ui/label.tsx";

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

export function CategoryForm({type}: {type: 'create' | 'edit'}) {
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
    } = useSWRMutation(
        ['/api/v1/categories', token],
        ([url, token], arg) => fetcher(url, token, arg),
        {}
    );

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
    
    const title = type === 'create' ? 'Create category' : 'Edit category';

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
                                    <div className="grid w-full max-w-sm items-center gap-1.5">
                                        <Label htmlFor="name">Category name</Label>
                                        <Input type="name" id="name" placeholder="Category name" {...field}/>
                                    </div>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                </form>
            </Form>
        </div>
    );
}
