import {CategoryDto} from "@/models/categories/CategoryDto.ts";

export interface TransactionDto {
    id: string;
    description: string;
    amount: number;
    type: string;
    date: number;
    categories: CategoryDto[];
}
