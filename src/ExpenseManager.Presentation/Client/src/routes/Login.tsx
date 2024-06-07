import BaseLayout from "@/layouts/BaseLayout.tsx";
import {LoginCard} from "@/components/auth/LoginCard.tsx";

export default function Login() {
    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <LoginCard/>
            </div>
        </BaseLayout>
    );
}
