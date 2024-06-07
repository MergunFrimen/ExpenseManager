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
        port: 3000,
        proxy: {
            "/api": {
                target: "http://127.0.0.1:5222",
                rewrite: (path) => path.replace(/^\/api/, ""),
                changeOrigin: true,
                followRedirects: true,
            },
        },
    }
})
