import useSWRMutation from 'swr/mutation'
import {ImportDto} from "@/models/import/ImportDto.ts";
import {DownloadIcon, UploadIcon} from 'lucide-react';
import {Input} from "@/components/ui/input.tsx";
import {Button} from "@/components/ui/button.tsx";
import {useRef} from 'react';
import {DropdownMenuItem} from "@/components/ui/dropdown-menu.tsx";

const fetcher = (url: string, {arg}: { arg: ImportDto }) => {
    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0NzlmMjkzZi1iY2Y3LTRmMDgtOThkOC1jNjk3Y2FkZTZkM2UiLCJnaXZlbl9uYW1lIjoiRG9taW5payIsImZhbWlseV9uYW1lIjoiVGljaHkiLCJqdGkiOiJjNWVjMWJiMi0wZTg1LTQxZDMtODI5Yy0zZTZkZGQ1MzBjM2IiLCJpc3MiOiJFeHBlbnNlTWFuYWdlciIsImF1ZCI6IkV4cGVuc2VNYW5hZ2VyIiwiZXhwIjoxNzE4ODQ0MzMzfQ.KX20Dz1q1ghopNcZT0_EeH1i35wl1-zuw9u5tv1r32s"
        },
        body: JSON.stringify(arg)
    }).then((res) => res.json())
}

export function Import() {
    const fileInputRef = useRef<HTMLInputElement>(null);
    const {data, trigger, isMutating, error} = useSWRMutation("/api/v1/import", fetcher);

    if (error) return <div>Failed to load</div>
    
    return (
        <Button onClick={() => {
            if (fileInputRef.current) {
                fileInputRef.current.click();
            }
        }}>
            <DownloadIcon className="mr-2 h-4 w-4"/>
            {isMutating ? "Loading..." : "Import"}
            <div className={'hidden'}>
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
                    }
                }}/>
            </div>
        </Button>
    )
}