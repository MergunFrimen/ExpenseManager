import axios, {AxiosResponse} from "axios";
import {CategoryDto} from "@/models/categories/CategoryDto.ts";
import {ListCategoriesResponse} from "@/models/categories/ListCategoriesResponse.ts";

export const transactionsApiConnector = {
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
    getCategorys: async () => {
        try {

            const response: AxiosResponse<ListCategoriesResponse> =
                await axios.get(`/api/categories`);
            return response.data.categories;
        } catch (error) {
            console.log('Error in getCategorys: ', error);
            throw error;
        }
    }
}