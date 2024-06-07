import axios, {AxiosResponse} from "axios";
import {ListTransactionsResponse} from "@/models/transactions/ListTransactionsResponse.ts";
import {TransactionDto} from "@/models/transactions/TransactionDto.ts";

export const transactionsApiConnector = {
    createTransaction: async (transaction: TransactionDto) => {
        try {
            const response: AxiosResponse<TransactionDto> =
                await axios.post(`/api/transactions`, transaction)
            return response.data;
        } catch (error) {
            console.log('Error in createTransaction: ', error);
            throw error;
        }
    },
    updateTransaction: async (transaction: TransactionDto) => {
        try {
            const response: AxiosResponse<TransactionDto> =
                await axios.put(`/api/transactions`, transaction);
            return response.data;
        } catch (error) {
            console.log('Error in updateTransaction: ', error);
            throw error;
        }
    },
    deleteTransaction: async (token: string, transaction: TransactionDto) => {
        try {
            const response: AxiosResponse<TransactionDto> =
                await axios.delete(`/api/transactions`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    },
                    data: transaction
                });
            return response.data;
        } catch (error) {
            console.log('Error in deleteTransaction: ', error);
            throw error;
        }
    },
    getTransactions: async (token: string) => {
        try {
            const response: AxiosResponse<ListTransactionsResponse> =
                await axios.get(`/api/transactions`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
            return response.data;
        } catch (error) {
            console.log('Error in getTransactions: ', error);
            throw error;
        }
    }
}