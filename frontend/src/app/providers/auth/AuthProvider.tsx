import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { httpClient } from "@/shared/api/httpClient";

export default function AuthProvider({ children }: { children: React.ReactNode }) {
    const { getAccessTokenSilently, isAuthenticated, isLoading, loginWithRedirect } = useAuth0();
    const [isInterceptorReady, setIsInterceptorReady] = useState(false);

    useEffect(() => {
        if (isLoading) return;

        if (!isAuthenticated) {
            setIsInterceptorReady(true);
            return;
        }

        const interceptorId = httpClient.interceptors.request.use(async (config) => {
            try {
                const token = await getAccessTokenSilently();
                config.headers.Authorization = `Bearer ${token}`;
                return config;
            } catch (error) {
                await loginWithRedirect();
                throw new Error(`Error fetching token: ${error}`);
            }
        });

        setIsInterceptorReady(true);

        return () => {
            httpClient.interceptors.request.eject(interceptorId);
            setIsInterceptorReady(false);
        };
    }, [getAccessTokenSilently, isAuthenticated, isLoading]);

    if (!isInterceptorReady) return null;

    return <>{children}</>;
}