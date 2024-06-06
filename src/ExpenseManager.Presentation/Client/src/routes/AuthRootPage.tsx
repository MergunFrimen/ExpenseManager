import {Outlet} from "react-router-dom";

export default function AuthRootPage() {
    // TODO: redirect to login page if not authenticated
    return (
        <>
            <h1>AuthRootPage</h1>
            <Outlet/>
        </>
    )
}