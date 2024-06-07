import BaseLayout from "@/layouts/BaseLayout.tsx";
import {LoginCard} from "@/components/auth/LoginCard.tsx";
import {Navigate} from "react-router-dom";
import {useAuth} from "@/components/auth/AuthProvider.tsx";

export default function Login() {
    const {token} = useAuth();

    // If the user is authenticated, redirect to the dashboard
    if (token) {
        return <Navigate to="/app"/>;
    }

    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <LoginCard/>
            </div>
        </BaseLayout>
    );
}
