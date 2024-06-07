import {RegisterCard} from "@/components/auth/RegisterCard.tsx";
import BaseLayout from "@/layouts/BaseLayout.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Navigate} from "react-router-dom";

export default function Register() {
    const {token} = useAuth();

    // If the user is authenticated, redirect to the dashboard
    if (token) {
        return <Navigate to="/app"/>;
    }

    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <RegisterCard/>
            </div>
        </BaseLayout>
    );
}
