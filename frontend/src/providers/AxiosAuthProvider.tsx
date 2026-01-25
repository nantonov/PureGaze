import { useAuth0 } from "@auth0/auth0-react";
import { httpClient } from "@/api/httpClient";
import { useEffect } from "react";

export default function AxiosAuthProvider({ children }: { children: React.ReactNode }) {
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const interceptorId = httpClient.interceptors.request.use(async (config) => {
      const token = await getAccessTokenSilently();
      config.headers.Authorization = `Bearer ${token}`;
      return config;
    });

    return () => {
      httpClient.interceptors.request.eject(interceptorId);
    };
  }, [getAccessTokenSilently]);

  return <>{children}</>;
}
