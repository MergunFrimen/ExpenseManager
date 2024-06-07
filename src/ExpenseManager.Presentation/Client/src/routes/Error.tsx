import BaseLayout from "@/layouts/BaseLayout.tsx";

export default function Error() {
    return <BaseLayout>
        <div className="flex flex-col items-center justify-center size-full space-y-6">

            <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
                404 Not Found
            </h1>
        </div>
    </BaseLayout>
}
