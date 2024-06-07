import {RegisterCard} from "@/components/auth/RegisterCard.tsx";
import BaseLayout from "@/layouts/BaseLayout.tsx";

export default function Register() {
    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <RegisterCard/>
            </div>
        </BaseLayout>
    );
}
