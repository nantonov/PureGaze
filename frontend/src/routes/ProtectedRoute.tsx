import { useAuth0 } from "@auth0/auth0-react";
import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useEmployee } from "@/contexts/EmployeeContext";
import { Loading } from "@/shared/components/Loading/Loading";
import { useBootstrapEmployee } from "@/shared/hooks/useBootstrapEmployee";
import { ROUTES } from "@/shared/constants";

interface Props {
  children: ReactNode;
}

export default function ProtectedRoute({ children }: Props) {
  const { isAuthenticated, isLoading } = useAuth0();
  const { employee } = useEmployee();
  const { loading } = useBootstrapEmployee();

  if (isLoading || loading) {
    return <Loading />;
  }

  if (!isAuthenticated) {
  return <Navigate to={ROUTES.LOGIN} replace />;
  }

  if (!employee) {
    return <Loading />;
  }

  return <>{children}</>;
}
