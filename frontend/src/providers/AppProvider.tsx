import { router } from "@/routes/router";
import { RouterProvider } from "react-router-dom";
import { ThemeProvider } from "@/contexts/ThemeContext";
import { EmployeeProvider } from "@/contexts/EmployeeContext";
import { LanguageProvider } from "@/contexts/LanguageContext";
import { Auth0Provider } from "@auth0/auth0-react";
import { auth0Config } from "@/config/auth0";
import AxiosAuthProvider from "@/providers/AxiosAuthProvider";

export default function AppProvider() {
  return (
    <Auth0Provider {...auth0Config}>
      <AxiosAuthProvider>
        <EmployeeProvider>
          <ThemeProvider>
            <LanguageProvider>
              <RouterProvider router={router} />
            </LanguageProvider>
          </ThemeProvider>
        </EmployeeProvider>
      </AxiosAuthProvider>
    </Auth0Provider>
  );
}
