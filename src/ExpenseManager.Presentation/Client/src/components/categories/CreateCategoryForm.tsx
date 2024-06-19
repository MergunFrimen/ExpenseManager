import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Form, FormControl, FormField, FormItem, FormMessage} from "@/components/ui/form.tsx";
import {Input} from "@/components/ui/input.tsx";
import {useForm} from "react-hook-form";
import {zodResolver} from "@hookform/resolvers/zod";
import {useToast} from "../ui/use-toast";
import useSWRMutation from "swr/mutation";
import {useEffect} from "react";
import {z} from "zod";
import {Label} from "@/components/ui/label.tsx";
import {Button} from "@/components/ui/button.tsx";
import {DialogFooter, DialogHeader, DialogTitle} from "@/components/ui/dialog.tsx";

const formSchema = z.object({
    name: z.string().min(1).max(50)
})

type FormFields = z.infer<typeof formSchema>;

async function fetcher(url: string, token: string | null, {arg}: { arg: { filters: { name?: string } } }) {
    const response = await fetch(url, {
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

export function CreateCategoryForm({setOpen}: {
    setOpen: (open: boolean) => void
}) {
    const {token} = useAuth();
    const {toast} = useToast()
    const form = useForm<FormFields>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: ''
        },
    })
    const {
        trigger,
        error
    } = useSWRMutation(
        [`/api/v1/categories`, token],
        ([url, token], arg) => fetcher(url, token, arg),
        {}
    );

    function onSubmit(e: FormFields) {
        trigger(e);
    }

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

    return (
        <div className="flex flex-col gap-y-3">
            <DialogHeader>
                <DialogTitle>Create category</DialogTitle>
            </DialogHeader>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}
                      className="space-y-8 flex flex-col size-full items-center justify-center">
                    <FormField
                        control={form.control}
                        name="name"
                        render={({field}) => (
                            <FormItem className="size-full">
                                {/*<FormLabel>Category name</FormLabel>*/}
                                <FormControl>
                                    <div className="grid w-full max-w-sm items-center gap-1.5">
                                        <Label htmlFor="name">Name</Label>
                                        <Input type="name" id="name" placeholder="" {...field}/>
                                    </div>
                                </FormControl>
                                <FormMessage/>
                            </FormItem>
                        )}
                    />
                    <Button type="submit" className={'w-full'} onClick={() => {
                        setOpen(false)
                    }}>
                        Submit
                    </Button>
                </form>
            </Form>
            <DialogFooter>
            </DialogFooter>
        </div>
    );
}
