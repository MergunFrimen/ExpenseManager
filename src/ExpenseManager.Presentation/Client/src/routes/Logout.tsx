import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useEffect} from "react";
import {Navigate} from "react-router-dom";

export const Logout = () => {
    const {setToken} = useAuth();

    useEffect(() => {
        setToken(null);
    }, []);

    return <Navigate to="/login"/>;
};