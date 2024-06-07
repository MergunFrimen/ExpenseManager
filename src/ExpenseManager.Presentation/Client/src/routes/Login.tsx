import BaseLayout from "@/layouts/BaseLayout.tsx";
import {useNavigate} from "react-router-dom";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {LoginCard} from "@/components/auth/LoginCard.tsx";

export default function Login() {
    const {setToken} = useAuth();
    const navigate = useNavigate();

    // TODO: do authentication here
    // const handleLogin = () => {
    //     setToken("this is a test token");
    //     navigate("/app", {replace: true});
    // };
    //
    // setTimeout(() => {
    //     handleLogin();
    // }, 1000);

    return (
        <BaseLayout>
            <div className="flex flex-col items-center justify-center size-full space-y-6">
                <LoginCard/>
            </div>
        </BaseLayout>
    );
}
