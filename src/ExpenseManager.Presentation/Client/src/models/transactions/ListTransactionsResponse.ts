import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export interface ListTransactionsResponse {
    transactions: TransactionDto[];
}