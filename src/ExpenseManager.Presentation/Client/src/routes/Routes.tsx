import {createBrowserRouter, RouteObject, RouterProvider} from "react-router-dom";
import Login from "@/routes/Login.tsx";
import Register from "@/routes/Register.tsx";
import Error from "@/routes/Error.tsx";
import Stats from "@/routes/Stats.tsx";
import {Logout} from "@/routes/Logout.tsx";
import App from "@/routes/App.tsx";

export function Routes() {
    const routes: RouteObject[] = [
            {
                path: "/",
                element: <App/>,
            },
            {
                path: "stats",
                element: <Stats/>,
            },
            {
                path: "login",
                element: <Login/>,
            },
            {
                path: "register",
                element: <Register/>,
            },
            {
                path: "logout",
                element: <Logout/>,
            },
            {
                path: "*",
                element: <Error/>,
            }
        ]
    ;

    const router = createBrowserRouter([
        ...routes
    ]);

    return <RouterProvider router={router}/>;
}
