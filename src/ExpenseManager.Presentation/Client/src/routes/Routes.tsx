import {createBrowserRouter, RouteObject, RouterProvider} from "react-router-dom";
import Home from "@/routes/Home.tsx";
import Login from "@/routes/Login.tsx";
import Register from "@/routes/Register.tsx";
import {ProtectedRoute} from "@/routes/ProtectedRoute.tsx";
import Error from "@/routes/Error.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import App from "@/routes/App.tsx";
import {Logout} from "@/routes/Logout.tsx";

export function Routes() {
    const { token } = useAuth();

    const publicRoutes: RouteObject[] = [
        {
            path: "/",
            element: <Home/>,
            errorElement: <Error/>,
        }
    ];

    const notAuthRoutes: RouteObject[] = [
        {
            path: "login",
            element: <Login/>,
        },
        {
            path: "register",
            element: <Register/>,
        },
    ];

    const authRoutes: RouteObject[] = [
        {
            path: "/",
            element: <ProtectedRoute />,
            errorElement: <Error/>,
            children: [
                {
                    path: "app",
                    element: <App/>,
                },
                {
                    path: "logout",
                    element: <Logout/>,
                }
            ],
        },
    ];
    
    const router = createBrowserRouter([
        ...publicRoutes,
        ...(!token ? notAuthRoutes : []),
        ...authRoutes,
    ]);

    return <RouterProvider router={router} />;
};
