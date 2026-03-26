import { createContext, useContext, useState } from "react";
import type { CurrentEmployee } from "@/types/Employees/CurrentEmployee.ts";

interface EmployeeContextValue {
  employee: CurrentEmployee | null;
  setEmployee: (e: CurrentEmployee | null) => void;
}

const EmployeeContext = createContext<EmployeeContextValue | undefined>(undefined);

export const EmployeeProvider = ({ children }: { children: React.ReactNode }) => {
  const [employee, setEmployee] = useState<CurrentEmployee | null>(null);

  return (
    <EmployeeContext.Provider value={{ employee, setEmployee }}>
      {children}
    </EmployeeContext.Provider>
  );
};

export const useEmployee = () => {
  const ctx = useContext(EmployeeContext);
  if (!ctx) throw new Error("useEmployee must be used inside EmployeeProvider");
  return ctx;
};
