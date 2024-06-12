import BaseLayout from "@/layouts/BaseLayout.tsx";
import {Card, CardContent, CardDescription, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import {SubmitHandler, useForm} from "react-hook-form";
import {Check, LoaderCircle} from "lucide-react";
import {z} from "zod";
import {zodResolver} from "@hookform/resolvers/zod";
import {Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from "@/components/ui/form.tsx";
import {Link} from "react-router-dom";

const schema = z.object({
    firstName: z.string().min(2).max(50).regex(/^[a-zA-Z]+$/, {message: "First name must only contain letters"}),
    lastName: z.string().min(2).max(50).regex(/^[a-zA-Z]+$/, {message: "Last name must only contain letters"}),
    email: z.string().email().max(150),
    password: z.string().min(8),
});

type FormFields = z.infer<typeof schema>;

export default function Register() {
    // const {token} = useAuth();
    //
    // // If the user is authenticated, redirect to the dashboard
    // if (token) {
    //     return <Navigate to="/app"/>;
    // }
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
        handleSubmit,
        formState: {
            errors,
            isSubmitting,
            isSubmitSuccessful
        }
    } = form;


    const onSubmit: SubmitHandler<FormFields> = async (data) => {
        try {
            await new Promise((resolve) => setTimeout(resolve, 1000));
            // throw new Error();
            console.log(data);
        } catch (error) {
            form.setError("root", {
                message: "Invalid all",
            });
        }
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
                                {errors.root && (
                                    <div className="text-sm font-medium text-destructive">
                                        {errors.root.message}
                                    </div>
                                )}
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
                                {/*TODO: fix this monstrosity*/}
                                <Button type="submit" className="w-full"
                                        disabled={isSubmitting || isSubmitSuccessful}>
                                    {isSubmitting && (<div className={"flex flex-row"}>
                                        <LoaderCircle className="animate-spin h-5 w-5 mr-3"/>
                                        <span className="">Registering</span>
                                    </div>)}

                                    {(!isSubmitting && !isSubmitSuccessful) &&
                                        (<span className={""}>Register</span>)
                                    }
                                    {(!isSubmitting && isSubmitSuccessful) &&
                                        (<div>
                                            < Check className="h-5 w-5 mr-3"/>
                                        </div>)
                                    }
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
