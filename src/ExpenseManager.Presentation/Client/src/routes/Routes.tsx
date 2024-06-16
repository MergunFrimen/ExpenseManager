import {createBrowserRouter, RouteObject, RouterProvider} from "react-router-dom";
import Home from "@/routes/Home.tsx";
import Login from "@/routes/Login.tsx";
import Register from "@/routes/Register.tsx";
import {ProtectedRoute} from "@/routes/ProtectedRoute.tsx";
import Error from "@/routes/Error.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import Stats from "@/routes/Stats.tsx";

export function Routes() {
    const {token} = useAuth();

    const publicRoutes: RouteObject[] = [
        {
            path: "/",
            element: <Home/>,
            errorElement: <Error/>,
        },
        {
            path: "login",
            element: <Login/>,
        },
        {
            path: "register",
            element: <Register/>,
        },
    ];

    const protectedRoutes: RouteObject[] = [
        {
            path: "/app",
            element: <ProtectedRoute/>,
            errorElement: <Error/>,
            children: [
                // {
                //     path: "logout",
                //     element: <Logout/>,
                // },
                {
                    path: "stats",
                    element: <Stats/>,
                }
                // {
                //     path: "createTransaction",
                //     element: <TransactionForm/>,
                // },
                // {
                //     path: "updateTransaction/:id",
                //     element: <TransactionForm/>,
                // },
                // {
                //     path: "createCategory",
                //     element: <>createCategory</>,
                // },
                // {
                //     path: "updateCategory/:id",
                //     element: <>editCategory</>,
                // }
            ],
        },
    ];

    const router = createBrowserRouter([
        ...publicRoutes,
        ...protectedRoutes,
    ]);

    return <RouterProvider router={router}/>;
}
