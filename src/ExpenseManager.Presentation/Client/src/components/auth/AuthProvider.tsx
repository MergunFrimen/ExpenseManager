import axios from "axios";
import {createContext, ReactNode, useContext, useEffect, useMemo, useState} from "react";

type AuthProviderProps = {
    children: ReactNode
    storageKey?: string
}

type AuthProviderState = {
    token: string | null,
    setToken: (token: string | null) => void
}

const initialState: AuthProviderState = {
    token: null,
    setToken: () => null
}

const AuthContext = createContext(initialState);

export const AuthProvider = ({
                                 children,
                                 storageKey = "jwt-token",
                                 ...props
                             }: AuthProviderProps) => {
    const [token, _setToken] = useState<string | null>(
        () => localStorage.getItem(storageKey)
    )

    useEffect(() => {
        if (token) {
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
            localStorage.setItem(storageKey, token);
        } else {
            delete axios.defaults.headers.common["Authorization"];
            localStorage.removeItem(storageKey)
        }
    }, [token]);

    const setToken = (newToken: string | null) => {
        _setToken(newToken);
    };

    const value = useMemo(
        () => ({
            token,
            setToken,
        }),
        [token]
    );

    return (
        <AuthContext.Provider {...props} value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);

    if (context === undefined)
        throw new Error("useAuth must be used within a AuthProvider");

    return context
};