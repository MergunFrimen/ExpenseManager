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
import {useAuth} from "@/components/auth/AuthProvider.tsx";

const schema = z.object({
    firstName: z.string().min(2).max(50).regex(/^[a-zA-Z]+$/, {message: "First name must only contain letters"}),
    lastName: z.string().min(2).max(50).regex(/^[a-zA-Z]+$/, {message: "Last name must only contain letters"}),
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

export default function Register() {
    const {token, setToken} = useAuth();

    const form = useForm<FormFields>({
        defaultValues: {
            firstName: "",
            lastName: "",
            email: "",
            password: "",
        },
        resolver: zodResolver(schema),
    });
    const {
        control,
        handleSubmit
    } = form;

    const {trigger, isMutating, error} = useSWRMutation("/api/v1/auth/register", fetcher);

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
                        <CardTitle className="text-2xl">Register</CardTitle>
                        <CardDescription>
                            Enter your information to create an account
                        </CardDescription>
                    </CardHeader>
                    <CardContent>
                        <Form {...form}>
                            <form onSubmit={handleSubmit(onSubmit)} className="grid gap-4">
                                <div className="grid grid-cols-2 gap-4">
                                    <FormField
                                        control={control}
                                        name="firstName"
                                        render={({field}) => (
                                            <FormItem>
                                                <FormLabel>First name</FormLabel>
                                                <FormControl>
                                                    <Input
                                                        className={"w-full"}
                                                        placeholder="John"
                                                        type={"text"}
                                                        {...field} />
                                                </FormControl>
                                                <FormMessage/>
                                            </FormItem>
                                        )}
                                    />
                                    <FormField
                                        control={control}
                                        name="lastName"
                                        render={({field}) => (
                                            <FormItem>
                                                <FormLabel>Last name</FormLabel>
                                                <FormControl>
                                                    <Input
                                                        className={"w-full"}
                                                        placeholder="Doe"
                                                        type={"text"}
                                                        {...field} />
                                                </FormControl>
                                                <FormMessage/>
                                            </FormItem>
                                        )}
                                    />
                                </div>
                                <FormField
                                    control={control}
                                    name="email"
                                    render={({field}) => (
                                        <FormItem>
                                            <FormLabel>Email</FormLabel>
                                            <FormControl>
                                                <Input
                                                    className={"w-full"}
                                                    placeholder="example@email.com"
                                                    type={"text"}
                                                    {...field} />
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
                                    Register
                                </Button>
                            </form>
                            <div className="mt-4 text-center text-sm">
                                Already have an account?{" "}
                                <Link to="/login" className="underline">
                                    Sign in
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
