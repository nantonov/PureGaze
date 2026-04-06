import { useAuth0 } from "@auth0/auth0-react";
import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useEmployee } from "@/app/providers/employee/EmployeeProvider.tsx";
import { Loading } from "@/shared/ui/spiner/Loading.tsx";
import { useBootstrapEmployee } from "@/shared/hooks/useBootstrapEmployee";
import { ROUTES } from "@/shared/constants";

interface Props {
  children: ReactNode;
}

export default function ProtectedRoute({ children }: Props) {
  const { isAuthenticated, isLoading } = useAuth0();
  const { employee } = useEmployee();
  const { loading, error } = useBootstrapEmployee();
  if (isLoading || loading) {
    return <Loading />;
  }

  if (!isAuthenticated) {
    return <Navigate to={ROUTES.LOGIN} replace />;
  }

  if (!employee || error) {
    return <Loading />;
  }

  return <>{children}</>;
}
