export interface TransactionDto {
    id: string;
    userId: string;
    type: string;
    category: string;
    description: string;
    amount: number;
    date: string;
}