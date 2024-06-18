export interface TransactionDto {
    id: string;
    description: string;
    amount: number;
    type: string;
    date?: number;
    categoryIds: string[];
    categoryNames: string[];
}
