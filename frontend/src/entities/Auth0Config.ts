import type { AuthorizationParams } from "./AuthorizationParams";

export interface Auth0Config {
  domain: string;
  clientId: string;
  cacheLocation: "memory" | "localstorage";
  authorizationParams: AuthorizationParams;
}
