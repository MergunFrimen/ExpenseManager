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
    email: z.string().email().max(150),
    password: z.string().min(8),
});

type FormFields = z.infer<typeof schema>;

export default function Login() {
    // const {token} = useAuth();
    //
    // // If the user is authenticated, redirect to the dashboard
    // if (token) {
    //     return <Navigate to="/app"/>;
    // }
    // const navigate = useNavigate();
    const form = useForm<FormFields>({
        defaultValues: {
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


    const onSubmit: SubmitHandler<FormFields> = async () => {
        try {
            await new Promise((resolve) => setTimeout(resolve, 1000));
            // throw new Error();
            // navigate("/my-route", { replace: true });
        } catch (error) {
            form.setError("root", {
                message: "Invalid email or password",
            });
        }
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
                                {errors.root && (
                                    <div className="text-sm font-medium text-destructive">
                                        {errors.root.message}
                                    </div>
                                )}
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
                                {/*TODO: fix this monstrosity*/}
                                <Button type="submit" className="w-full" disabled={isSubmitting || isSubmitSuccessful}>
                                    {isSubmitting && (<div className={"flex flex-row"}>
                                        <LoaderCircle className="animate-spin h-5 w-5 mr-3"/>
                                        <span className="">Logging in</span>
                                    </div>)}

                                    {(!isSubmitting && !isSubmitSuccessful) &&
                                        (<span className={""}>Login</span>)
                                    }
                                    {(!isSubmitting && isSubmitSuccessful) &&
                                        (<div>
                                            < Check className="h-5 w-5 mr-3"/>
                                        </div>)
                                    }
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
