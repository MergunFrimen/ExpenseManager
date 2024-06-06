import ReactDOM from 'react-dom/client'
import {RouterProvider} from "react-router-dom";
import React from "react";
import router from "@/routes/router.tsx";
import './index.css'

const root = document.getElementById('root');

ReactDOM.createRoot(root!).render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
)
