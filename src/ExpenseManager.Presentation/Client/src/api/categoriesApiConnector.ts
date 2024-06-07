import axios, {AxiosResponse} from "axios";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {ListCategoriesResponse} from "@/models/categories/ListCategoriesResponse.ts";
import {ListTransactionsResponse} from "@/models/transactions/ListTransactionsResponse.ts";

export const categoriesApiConnector = {
    createCategory: async (category: CategoryDto) => {
        try {
            const response: AxiosResponse<CategoryDto> =
                await axios.post(`/api/categories`, category);
            return response.data;
        } catch (error) {
            console.log('Error in createCategory: ', error);
            throw error;
        }
    },
    updateCategory: async (category: CategoryDto) => {
        try {
            const response: AxiosResponse<CategoryDto> =
                await axios.put(`/api/categories`, category);
            return response.data;
        } catch (error) {
            console.log('Error in updateCategory: ', error);
            throw error;
        }
    },
    deleteCategory: async (category: CategoryDto) => {
        try {
            const response: AxiosResponse<CategoryDto> =
                await axios.delete(`/api/categories`, {data: category});
            return response.data;
        } catch (error) {
            console.log('Error in deleteCategory: ', error);
            throw error;
        }
    },
    getCategories: async (token: string) => {
        try {

            const response: AxiosResponse<ListCategoriesResponse> =
                await axios.get(`/api/categories`, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
            return response.data;
        } catch (error) {
            console.log('Error in getCategories: ', error);
            throw error;
        }
    },
}