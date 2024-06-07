export interface TransactionDto {
    id: string;
    type: string;
    categoryId: string;
    category: string;
    description: string;
    amount: number;
    date: string;
}
