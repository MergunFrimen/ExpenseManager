import {Navigate, Outlet, useLocation} from "react-router-dom";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import Dashboard from "@/routes/Dashboard.tsx";

export const ProtectedRoute = () => {
    const {token} = useAuth();
    const location = useLocation();

    // TODO: reenable when done
    // If the user is not authenticated, redirect to the login page
    // if (!token) {
    //     return <Navigate to="/login"/>;
    // }
    //
    // if (location.pathname === '/app') {
    //     return <Dashboard/>
    // }

    return <Outlet/>
};
