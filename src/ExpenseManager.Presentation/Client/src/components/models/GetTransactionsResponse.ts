import {TransactionDto} from "@/components/models/TransactionDto.ts";

export interface GetTransactionsResponse {
    transactions: TransactionDto[];
}