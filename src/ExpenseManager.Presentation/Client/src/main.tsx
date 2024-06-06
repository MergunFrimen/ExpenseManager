import ReactDOM from 'react-dom/client'
import {RouterProvider} from "react-router-dom";
import React from "react";
import router from "@/router.tsx";
import './index.css'

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement,
);

root.render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
)
