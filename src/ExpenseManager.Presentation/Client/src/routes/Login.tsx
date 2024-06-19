import BaseLayout from "@/layouts/BaseLayout.tsx";
import {Card, CardContent, CardDescription, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useForm} from "react-hook-form";
import {z} from "zod";
import {zodResolver} from "@hookform/resolvers/zod";
import {Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from "@/components/ui/form.tsx";
import {Link, Navigate} from "react-router-dom";
import {toast} from "@/components/ui/use-toast.ts";
import {useEffect} from "react";
import useSWRMutation from "swr/mutation";
import {useAuth} from "@/components/auth/AuthProvider";

const schema = z.object({
    email: z.string().email().max(150),
    password: z.string().min(8),
});

type FormFields = z.infer<typeof schema>;

async function fetcher(url: string, {arg}: { arg: FormFields }) {
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(arg)
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export default function Login() {
    const {token, setToken} = useAuth();

    const form = useForm<FormFields>({
        defaultValues: {
            // TODO: change back after testing
            // email: "",
            // password: "",
            email: "dominik@tichy.cz",
            password: "Pa$$word1"
        },
        resolver: zodResolver(schema),
    });
    const {
        control,
        handleSubmit
    } = form;

    const {trigger, isMutating, error} = useSWRMutation("/api/v1/auth/login", fetcher);

    useEffect(() => {
        if (error) {
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
        }
    }, [error]);

    async function onSubmit(e: FormFields) {
        const data = await trigger(e);
        setToken(data.token);
    }

    // If the user is authenticated, redirect to the dashboard
    if (token) {
        return <Navigate to="/"/>;
    }

    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <Card className="mx-auto max-w-sm">
                    <CardHeader>
                        <CardTitle className="text-2xl">Login</CardTitle>
                        <CardDescription>
                            Enter your email below to login to your account
                        </CardDescription>
                    </CardHeader>
                    <CardContent>
                        <Form {...form}>
                            <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
                                <FormField
                                    control={control}
                                    name="email"
                                    render={({field}) => (
                                        <FormItem>
                                            <FormLabel>Email</FormLabel>
                                            <FormControl>
                                                <Input placeholder="example@email.com" {...field} />
                                            </FormControl>
                                            <FormMessage/>
                                        </FormItem>
                                    )}
                                />
                                <FormField
                                    control={control}
                                    name="password"
                                    render={({field}) => (
                                        <FormItem>
                                            <FormLabel>Password</FormLabel>
                                            <FormControl>
                                                <Input
                                                    className={"w-full"}
                                                    placeholder="Password"
                                                    type={"password"}
                                                    {...field} />
                                            </FormControl>
                                            <FormMessage/>
                                        </FormItem>
                                    )}
                                />
                                <Button type="submit" className="w-full" disabled={isMutating}>
                                    Login
                                </Button>
                            </form>
                            <div className="mt-4 text-center text-sm">
                                Don&apos;t have an account?{" "}
                                <Link to="/register" className="underline">
                                    Sign up
                                </Link>
                            </div>
                        </Form>
                    </CardContent>
                </Card>
            </div>
        </BaseLayout>
    )
        ;
}
