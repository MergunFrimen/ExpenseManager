import {useEffect, useState} from "react";
import useSWRMutation from 'swr/mutation'
import {UploadIcon} from "lucide-react";
import {DropdownMenuItem} from "@/components/ui/dropdown-menu.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {toast} from "@/components/ui/use-toast.ts";

async function fetcher(url: string, token: string | null) {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function Export() {
    const {token} = useAuth();
    const {
        data,
        trigger,
        isMutating,
        error
    } = useSWRMutation(['/api/v1/export', token], ([url, token], arg) => fetcher(url, token, arg));
    const [downloadUrl, setDownloadUrl] = useState<string | undefined>(undefined);

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })
    }, [error]);

    useEffect(() => {
        if (data) {
            toast({
                variant: "default",
                title: "Export successful.",
            })
        }
    }, [data]);

    useEffect(() => {
        if (data) {
            const jsonString = JSON.stringify(data, null, 2); // Pretty-print with 2 spaces
            const blob = new Blob([jsonString], {type: "application/json"});
            const url = URL.createObjectURL(blob);
            setDownloadUrl(url);

            return () => {
                URL.revokeObjectURL(url); // Cleanup the object URL on unmount
            };
        }
    }, [data]);

    useEffect(() => {
        if (downloadUrl) {
            const a = document.createElement('a');
            a.href = downloadUrl;
            a.download = 'data.json';
            a.click();
        }
    }, [downloadUrl]);

    return <DropdownMenuItem className={''} onClick={() => trigger()} disabled={isMutating}>
        <UploadIcon className="mr-2 h-4 w-4"/>
        Export
    </DropdownMenuItem>
}