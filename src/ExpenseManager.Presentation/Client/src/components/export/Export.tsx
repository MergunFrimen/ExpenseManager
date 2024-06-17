import {useEffect, useState} from "react";
import useSWRMutation from 'swr/mutation'
import {UploadIcon} from "lucide-react";
import {DropdownMenuItem} from "@/components/ui/dropdown-menu.tsx";

const fetcher = (url: string) => fetch(url, {
    method: "GET",
    headers: {
        "Content-Type": "application/json",
        "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0NzlmMjkzZi1iY2Y3LTRmMDgtOThkOC1jNjk3Y2FkZTZkM2UiLCJnaXZlbl9uYW1lIjoiRG9taW5payIsImZhbWlseV9uYW1lIjoiVGljaHkiLCJqdGkiOiJjNWVjMWJiMi0wZTg1LTQxZDMtODI5Yy0zZTZkZGQ1MzBjM2IiLCJpc3MiOiJFeHBlbnNlTWFuYWdlciIsImF1ZCI6IkV4cGVuc2VNYW5hZ2VyIiwiZXhwIjoxNzE4ODQ0MzMzfQ.KX20Dz1q1ghopNcZT0_EeH1i35wl1-zuw9u5tv1r32s"
    },
}).then((res) => res.json());

export function Export() {
    const {data, trigger, isMutating, error} = useSWRMutation("/api/v1/export", fetcher);
    const [downloadUrl, setDownloadUrl] = useState<string | undefined>(undefined);

    if (error) return <div>Failed to load</div>


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
        {isMutating ? "Loading..." : "Export"}
    </DropdownMenuItem>
}