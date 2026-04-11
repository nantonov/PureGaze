import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useEmployee } from "@/app/providers/employee/EmployeeProvider.tsx";

interface Props {
  children: ReactNode;
  roles: string[];
}

export default function RoleProtectedRoute({ children, roles }: Props) {
  const { employee } = useEmployee();

  if (!employee?.managerLevel || !roles.includes(employee.managerLevel)) {
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
}
