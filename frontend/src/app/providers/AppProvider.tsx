import { router } from "@/app/router/router.tsx";
import { auth0Config } from "@/shared/config/auth0";
import { RouterProvider } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";
import { ThemeProvider } from "@/app/providers/theme/ThemeProvider.tsx";
import { EmployeeProvider } from "@/app/providers/employee/EmployeeProvider.tsx";
import { LanguageProvider } from "@/app/providers/i18n/LanguageProvider.tsx";
import AuthProvider from "@/app/providers/auth/AuthProvider";

export default function AppProvider() {
  return (
    <Auth0Provider {...auth0Config}>
      <AuthProvider>
        <EmployeeProvider>
          <ThemeProvider>
            <LanguageProvider>
              <RouterProvider router={router} />
            </LanguageProvider>
          </ThemeProvider>
        </EmployeeProvider>
      </AuthProvider>
    </Auth0Provider>
  );
}
