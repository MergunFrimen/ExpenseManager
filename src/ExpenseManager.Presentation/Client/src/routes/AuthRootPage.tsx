import {Outlet} from "react-router-dom";
import BaseLayout from "@/layouts/BaseLayout.tsx";

export default function AuthRootPage() {
    // TODO: redirect to login page if not authenticated
    
    return (
        <BaseLayout>
            <h1>AuthRootPage</h1>
            <Outlet/>
        </BaseLayout>
    )
}