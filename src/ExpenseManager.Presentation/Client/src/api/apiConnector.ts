import axios, {AxiosResponse} from "axios";
import {GetTransactionsResponse} from "@/components/models/GetTransactionsResponse.ts";

export const apiConnector = {
    getTransactions: async (userId: string) => {
        try {

            const response: AxiosResponse<GetTransactionsResponse> = await axios.get(`/api/users/${userId}/transactions`);
            return response.data.transactions;
        } catch (error) {
            console.log('Error in getTransactions: ', error);
            throw error;
        }
    },
    // createTransaction: async (transaction: TransactionDto) => {
    //     try {
    //
    //         const response: AxiosResponse<GetTransactionsResponse> = await axios.post(`/api/users/${userId}/transactions`, transaction);
    //         return response.data.transactions;
    //     } catch (error) {
    //         console.log('Error in getTransactions: ', error);
    //         throw error;
    //     }
    // },
}