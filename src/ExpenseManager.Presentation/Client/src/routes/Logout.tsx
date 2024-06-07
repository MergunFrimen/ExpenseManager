import {useAuth} from "@/components/auth/AuthProvider.tsx";

export const Logout = () => {
    const {setToken} = useAuth();

    setToken();

    return <></>
};