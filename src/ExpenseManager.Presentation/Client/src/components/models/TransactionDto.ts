export interface TransactionDto {
    id: string;
    userId: string;
    type: string;
    category: string;
    description: string;
    price: number;
    date: string;
}