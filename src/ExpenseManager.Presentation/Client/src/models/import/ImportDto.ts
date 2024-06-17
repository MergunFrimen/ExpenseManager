import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export interface ImportDto {
    transactions: TransactionDto[];
    categories: CategoryDto[];
}