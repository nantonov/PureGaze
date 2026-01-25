import { createContext, useContext, useState } from "react";

type Language = "en" | "ru";

const LanguageContext = createContext<{
  language: Language;
  setLanguage: (l: Language) => void;
}>({
  language: "en",
  setLanguage: () => {},
});

export const LanguageProvider = ({ children }: { children: React.ReactNode }) => {
  const [language, setLanguage] = useState<Language>("en");

  return (
    <LanguageContext.Provider value={{ language, setLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = () => useContext(LanguageContext);
