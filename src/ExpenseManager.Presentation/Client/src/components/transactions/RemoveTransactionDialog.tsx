import {DialogDescription, DialogFooter, DialogHeader, DialogTitle} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {toast} from "@/components/ui/use-toast.ts";
import useSWRMutation from "swr/mutation";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import {useEffect} from "react";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

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

export function RemoveTransactionDialog({transaction, setOpen}: { transaction: TransactionDto, setOpen: (open: boolean) => void }) {
    const {token} = useAuth();
    const {
        data,
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

    useEffect(() => {
    }, [data]);

    return <>
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
                onClick={() => {
                    trigger({transactionIds: transaction.id})
                    setOpen(false)
                }}
            >
                Delete
            </Button>
        </DialogFooter>
    </>
}