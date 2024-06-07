import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {transactionsApiConnector} from "@/api/transactionsApiConnector.ts";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export function TransactionForm() {
    const {id} = useParams();

    const [transaction, setTransaction] = useState<TransactionDto>({
        id: "",
        categoryId: "",
        type: "",
        category: "",
        description: "",
        amount: 0,
        date: ""
    });
    
    useEffect(() => {
        async function fetchTransaction(id: string) {
            const trans = await transactionsApiConnector.getTransactions(id);
            setTransaction(trans);
        }
        
        if (id) {
            fetchTransaction(id);
        }
    }, [id]);
    
    return (
        <div>
            <h1>Transaction Form</h1>
        </div>
    )
}