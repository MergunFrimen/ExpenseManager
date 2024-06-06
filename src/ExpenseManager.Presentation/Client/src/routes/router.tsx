import {createBrowserRouter} from "react-router-dom";
import RootPage from "@/routes/RootPage.tsx";
import ErrorPage from "@/routes/ErrorPage.tsx";
import LoginPage from "@/routes/LoginPage.tsx";
import RegisterPage from "@/routes/RegisterPage.tsx";
import AuthRootPage from "@/routes/AuthRootPage.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <RootPage />,
        errorElement: <ErrorPage />,
    },
    {
        path: "/auth",
        element: <AuthRootPage />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "login",
                element: <LoginPage />,
            },
            {
                path: "register",
                element: <RegisterPage />,
            },
        ],
    },
]);

export default router;