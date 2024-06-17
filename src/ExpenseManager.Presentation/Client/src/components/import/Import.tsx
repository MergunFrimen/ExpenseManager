import useSWRMutation from 'swr/mutation'
import {ImportDto} from "@/models/import/ImportDto.ts";
import {DownloadIcon} from 'lucide-react';
import {Input} from "@/components/ui/input.tsx";
import {useEffect, useRef} from 'react';
import {toast} from "@/components/ui/use-toast.ts";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {Button} from '../ui/button';
import { DropdownMenuItem } from '../ui/dropdown-menu';

async function fetcher(url: string, token: string | null, {arg}: { arg: ImportDto }) {
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify(arg)
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function Import() {
    const {token} = useAuth();
    const fileInputRef = useRef<HTMLInputElement>(null);
    const {
        data,
        trigger,
        isMutating,
        error
    } = useSWRMutation(['/api/v1/import', token], ([url, token], arg) => fetcher(url, token, arg));

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
                title: "Import successful.",
                description: `Imported ${data.amountCategoryAdded} transactions and ${data.amountCategoryAdded} categories.`,
            })
        }
    }, [data]);

    return (
        <div
            onClick={() => {
                if (fileInputRef.current) {
                    fileInputRef.current.click();
                }
            }}
            className={'relative hover:bg-accent hover:text-accent-foreground flex cursor-default select-none items-center rounded-sm px-2 py-1.5 text-sm outline-none transition-colors hover:cursor-pointer focus:bg-accent focus:text-accent-foreground data-[disabled]:pointer-events-none data-[disabled]:opacity-50'}
        >
            <DownloadIcon className="mr-2 h-4 w-4"/>
            {isMutating ? "Loading..." : "Import"}
            <div hidden className={'hidden'}>
                <Input ref={fileInputRef} id="picture" type="file" accept=".json" onChange={(e) => {
                    const file = e.target.files?.[0];
                    if (file) {
                        const reader = new FileReader();
                        reader.onload = () => {
                            const result = reader.result as string;
                            const data = JSON.parse(result);
                            trigger(data);
                        };
                        reader.readAsText(file);
                        // document.location.reload();
                    }
                }}/>
            </div>
        </div>
    )
}