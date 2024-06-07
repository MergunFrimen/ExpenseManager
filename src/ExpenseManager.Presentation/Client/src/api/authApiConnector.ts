import axios, {AxiosResponse} from "axios";
import {RegisterRequest} from "@/models/auth/RegisterRequest.ts";
import {AuthResponse} from "@/models/auth/AuthResponse.ts";
import {LoginRequest} from "@/models/auth/LoginRequest.ts";

export const transactionsApiConnector = {
    register: async (request: RegisterRequest) => {
        try {
            const response: AxiosResponse<AuthResponse> =
                await axios.post(`/api/auth/register`, request);
            return response.data;
        } catch (error) {
            console.log('Error in register: ', error);
            throw error;
        }
    },
    login: async (request: LoginRequest) => {
        try {
            const response: AxiosResponse<AuthResponse> =
                await axios.post(`/api/auth/login`, request);
            return response.data;
        } catch (error) {
            console.log('Error in login: ', error);
            throw error;
        }
    }
}