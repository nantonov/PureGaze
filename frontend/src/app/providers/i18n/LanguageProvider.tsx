import React, { createContext, useContext, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { employeeApi } from "@/features/employee/api/employeeApi";
import { useEmployee } from "@/app/providers/employee/EmployeeProvider.tsx";

export type Language = "en" | "ru";

export interface LanguageContext {
  language: Language;
  setLanguage: (l: Language) => void;
}

const LanguageContext = createContext<LanguageContext>({
  language: "en",
  setLanguage: () => {},
});

export const LanguageProvider = ({ children }: { children: React.ReactNode }) => {
  const { i18n } = useTranslation();
  const { employee, setEmployee } = useEmployee();

  const [language, setLanguageState] = useState<Language>(
    (i18n.resolvedLanguage as Language) || "en",
  );

  useEffect(() => {
    const sync = (lng: string) => setLanguageState(lng as Language);
    i18n.on("languageChanged", sync);
    return () => {
      i18n.off("languageChanged", sync);
    };
  }, [i18n]);

  useEffect(() => {
    if (!employee?.language) return;
    if (employee.language !== i18n.resolvedLanguage) {
      i18n.changeLanguage(employee.language);
    }
  }, [employee]);

  const setLanguage = async (l: Language) => {
    i18n.changeLanguage(l);

    if (!employee) return;
    setEmployee({ ...employee, language: l });
    try {
      await employeeApi.updateLanguage(l);
    } catch (err) {
      console.error("Failed to save language to backend", err);
    }
  };

  return (
    <LanguageContext.Provider value={{ language, setLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = () => useContext(LanguageContext);
