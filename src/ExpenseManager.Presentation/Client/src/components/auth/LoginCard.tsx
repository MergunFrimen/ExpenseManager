import {Button} from "@/components/ui/button"
import {Card, CardContent, CardDescription, CardHeader, CardTitle,} from "@/components/ui/card"
import {Input} from "@/components/ui/input"
import {Label} from "@/components/ui/label"
import {Link, useNavigate} from "react-router-dom";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useState} from "react";
import {authApiConnector} from "@/api/authApiConnector.ts";

export function LoginCard() {
    const navigate = useNavigate();
    const {setToken} = useAuth();
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    async function handleSubmit(e: any) {
        if (email === undefined || password === undefined) {
            return;
        }

        e.preventDefault();
        const response = await authApiConnector.login({
            email,
            password
        });
        const token = response.token;
        setToken(token);
        navigate('/app');
    }

    return (
        <Card className="mx-auto max-w-sm">
            <CardHeader>
                <CardTitle className="text-2xl">Login</CardTitle>
                <CardDescription>
                    Enter your email below to login to your account
                </CardDescription>
            </CardHeader>
            <CardContent>
                <div className="grid gap-4">
                    <div className="grid gap-2">
                        <Label htmlFor="email">Email</Label>
                        <Input
                            id="email"
                            type="email"
                            placeholder="example@email.com"
                            onChange={e => setEmail(e.target.value)}
                            required
                        />
                    </div>
                    <div className="grid gap-2">
                        <Label htmlFor="password">Password</Label>
                        <Input
                            id="password"
                            type="password"
                            onChange={e => setPassword(e.target.value)}
                            required
                        />
                    </div>
                    <Button type="submit" className="w-full" onClick={handleSubmit}>
                        Login
                    </Button>
                </div>
                <div className="mt-4 text-center text-sm">
                    Don&apos;t have an account?{" "}
                    <Link to="/register" className="underline">
                        Sign up
                    </Link>
                </div>
            </CardContent>
        </Card>
    )
}