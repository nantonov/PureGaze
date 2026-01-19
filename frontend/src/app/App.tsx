import "./App.css";
import { router } from "./router";
import { RouterProvider } from "react-router-dom";
import { ThemeProvider } from "./context/ThemeContext";
import { EmployeeProvider } from "./context/EmployeeContext";
import { LanguageProvider } from "./context/LanguageContext";

export default function App() {
  return( 
    <EmployeeProvider>
      <ThemeProvider>
        <LanguageProvider>
          <RouterProvider router={router} />
        </LanguageProvider>
      </ThemeProvider>
    </EmployeeProvider>
  );
}
