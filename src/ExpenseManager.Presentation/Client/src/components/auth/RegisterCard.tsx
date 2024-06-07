import {Button} from "@/components/ui/button"
import {Card, CardContent, CardDescription, CardHeader, CardTitle,} from "@/components/ui/card"
import {Input} from "@/components/ui/input"
import {Label} from "@/components/ui/label"
import {Link, useNavigate} from "react-router-dom";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useState} from "react";
import {authApiConnector} from "@/api/authApiConnector.ts";

export function RegisterCard() {
    const navigate = useNavigate();
    const {setToken} = useAuth();
    const [firstName, setFirstName] = useState();
    const [lastName, setLastName] = useState();
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    async function handleSubmit(e: any) {
        if (email === undefined || password === undefined || firstName === undefined || lastName === undefined) {
            return;
        }

        e.preventDefault();
        const response = await authApiConnector.register({
            email,
            password,
            firstName,
            lastName
        });
        const token = response.token;
        setToken(token);
        navigate('/app');
    }

    return (
        <Card className="mx-auto max-w-sm">
            <CardHeader>
                <CardTitle className="text-xl">Sign Up</CardTitle>
                <CardDescription>
                    Enter your information to create an account
                </CardDescription>
            </CardHeader>
            <CardContent>
                <div className="grid gap-4">
                    <div className="grid grid-cols-2 gap-4">
                        <div className="grid gap-2">
                            <Label htmlFor="first-name">First name</Label>
                            <Input id="first-name" placeholder="John" required
                                   onChange={e => setFirstName(e.target.value)}
                            />
                        </div>
                        <div className="grid gap-2">
                            <Label htmlFor="last-name">Last name</Label>
                            <Input id="last-name" placeholder="Doe" required
                                   onChange={e => setLastName(e.target.value)}

                            />
                        </div>
                    </div>
                    <div className="grid gap-2">
                        <Label htmlFor="email">Email</Label>
                        <Input
                            id="email"
                            type="email"
                            placeholder="example@email.com"
                            required
                            onChange={e => setEmail(e.target.value)}

                        />
                    </div>
                    <div className="grid gap-2">
                        <Label htmlFor="password">Password</Label>
                        <Input id="password" type="password"
                               onChange={e => setPassword(e.target.value)}
                        />
                    </div>
                    <Button type="submit" className="w-full" onClick={handleSubmit}>
                        Create an account
                    </Button>
                </div>
                <div className="mt-4 text-center text-sm">
                    Already have an account?{" "}
                    <Link to="/login" className="underline">
                        Sign in
                    </Link>
                </div>
            </CardContent>
        </Card>
    )
}
