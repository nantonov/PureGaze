import type { Auth0Config } from "../types/Auth0Config";

export const auth0Config: Auth0Config = {
  domain: import.meta.env.VITE_AUTH0_DOMAIN,
  clientId: import.meta.env.VITE_AUTH0_CLIENT_ID,
  cacheLocation: (import.meta.env.VITE_AUTH0_Cache_Location || "localstorage") as
    | "memory"
    | "localstorage",
  authorizationParams: {
    redirect_uri: window.location.origin,
    audience: import.meta.env.VITE_AUTH0_AUDIENCE,
  },
};
