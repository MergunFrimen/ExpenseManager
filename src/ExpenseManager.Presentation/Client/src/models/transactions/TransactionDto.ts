export interface TransactionDto {
    id: string;
    description: string;
    amount: number;
    type: 'Income' | 'Expense';
    date?: number;
    categoryIds: string[];
    categoryNames: string[];
}
