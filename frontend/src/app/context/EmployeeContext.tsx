import { createContext, useContext } from "react";
import type { Employee } from "../../types/Employee";

const mockEmployee: Employee = {
  id: 1,
  firstName: "Ivan",
  lastName: "Ivanov",
  email: "ivan@test.com",
  role: "admin",
};

const EmployeeContext = createContext<{
  employee: Employee | null;
}>({
  employee: null,
});

export const EmployeeProvider = ({ children }: { children: React.ReactNode }) => {
  return (
    <EmployeeContext.Provider value={{ employee: mockEmployee }}>
      {children}
    </EmployeeContext.Provider>
  );
};

export const useEmployee = () => useContext(EmployeeContext);
