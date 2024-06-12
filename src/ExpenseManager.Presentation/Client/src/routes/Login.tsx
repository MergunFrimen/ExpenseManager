import BaseLayout from "@/layouts/BaseLayout.tsx";
import {Card, CardContent, CardDescription, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {Label} from "@/components/ui/label.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import {SubmitHandler, useForm} from "react-hook-form";
import {LoaderCircle} from "lucide-react";
import {z} from "zod";
import {zodResolver} from "@hookform/resolvers/zod";

const schema = z.object({
    email: z.string().email(),
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
    //
    const {
        register,
        setError, handleSubmit,
        formState: {isSubmitting, errors}
    } = useForm<FormFields>({
        resolver: zodResolver(schema),
    });

    const onSubmit: SubmitHandler<FormFields> = async (data) => {
        try {
            await new Promise((resolve) => setTimeout(resolve, 1000));
            throw new Error();
            console.log(data);
        } catch (error) {
            setError("root", {
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
                        <form className="grid gap-4" onSubmit={handleSubmit(onSubmit)}>
                            {errors.root && (
                                <div className="text-red-500 text-sm">
                                    {errors.root.message}
                                </div>
                            )}
                            <div className="grid gap-2">
                                <Label htmlFor="email">Email</Label>
                                <Input
                                    {...register("email")}
                                    id="email"
                                    type="email"
                                    placeholder="example@email.com"
                                />
                                {errors.email && (
                                    <div className="text-red-500 text-sm">
                                        {errors.email.message}
                                    </div>
                                )}
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="password">Password</Label>
                                <Input
                                    {...register("password")}
                                    id="password"
                                    type="password"
                                    placeholder="Password"
                                />
                                {errors.password && (
                                    <div className="text-red-500 text-sm">
                                        {errors.password.message}
                                    </div>
                                )}

                            </div>
                            <Button type="submit" className="w-full" disabled={isSubmitting}>
                                {
                                    isSubmitting
                                        ? <>
                                            Logging in
                                            <LoaderCircle className="animate-spin h-5 w-5 ml-2"/>
                                        </>
                                        : "Login"
                                }
                            </Button>
                        </form>
                        {/*<div className="mt-4 text-center text-sm">*/}
                        {/*    Don&apos;t have an account?{" "}*/}
                        {/*    <Link to="/register" className="underline">*/}
                        {/*        Sign up*/}
                        {/*    </Link>*/}
                        {/*</div>*/}
                    </CardContent>
                </Card>
            </div>
        </BaseLayout>
    );
}
