import {
    Dialog,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {toast} from "@/components/ui/use-toast.ts";
import useSWRMutation from "swr/mutation";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useEffect} from "react";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";
import {Trash2} from "lucide-react";

async function fetcher(url: string, token: string | null, {arg}: { arg: { transactionIds: string } }) {
    const response = await fetch(url, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        },
        body: JSON.stringify({
            transactionIds: [arg.transactionIds]
        })
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function RemoveTransactionDialog({transaction}: {
    transaction: TransactionDto
}) {
    const {token} = useAuth();
    const {
        error,
        trigger
    } = useSWRMutation(['/api/v1/transactions', token], ([url, token], arg) => fetcher(url, token, arg));

    useEffect(() => {
        if (error)
            toast({
                variant: "destructive",
                title: "Uh oh! Something went wrong.",
                description: "There was a problem with your request.",
            })

    }, [error]);

    async function onSubmit() {
        trigger({transactionIds: transaction.id} as any);

        toast({
            title: "Deleted transaction.",
        })
    }

    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button variant="destructive" size="icon">
                    <Trash2 className="h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]">
                <DialogHeader>
                    <DialogTitle>Delete</DialogTitle>
                    <DialogDescription>
                        Are you sure you want to delete this transaction? This action cannot be undone.
                    </DialogDescription>
                </DialogHeader>
                <DialogFooter>
                    <Button
                        type="submit"
                        variant="destructive"
                        onClick={onSubmit}>
                        Delete
                    </Button>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    );
}