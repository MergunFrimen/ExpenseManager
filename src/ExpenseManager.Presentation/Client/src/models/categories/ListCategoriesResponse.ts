import {CategoryDto} from "@/models/categories/CategoryDto.ts";

export interface ListCategoriesResponse {
    categories: CategoryDto[];
}