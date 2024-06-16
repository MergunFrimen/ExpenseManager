export interface TransactionDto {
    id: string;
    type: string;
    description: string;
    amount: number;
    date: number;
    categoryIds: string[];
}
