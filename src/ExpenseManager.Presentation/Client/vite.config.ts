import {defineConfig} from "vite"
import react from "@vitejs/plugin-react"
import path from "path"

export default defineConfig({
    plugins: [react()],
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
        },
    },
    server: {
        host: "127.0.0.1",
        port: 5173,
        proxy: {
            "/api": {
                target: "http://127.0.0.1:5222",
                changeOrigin: true,
                followRedirects: true,
            },
        },
    },
    preview: {
        host: "127.0.0.1",
        port: 4173,
        proxy: {
            "/api": {
                target: "http://127.0.0.1:5222",
                changeOrigin: true,
                followRedirects: true,
            },
        },
    },
})
