import { useAuth0 } from "@auth0/auth0-react";
import { Navigate } from "react-router-dom";
import type { ReactNode } from "react";
import { useEmployee } from "@/contexts/EmployeeContext";
import { Loading } from "@/shared/components/Loading/Loading";
import { useBootstrapEmployee } from "@/shared/hooks/useBootstrapEmployee";

interface Props {
  children: ReactNode;
}

export default function ProtectedRoute({ children }: Props) {
  const { isAuthenticated, isLoading } = useAuth0();
  const { employee } = useEmployee();
  const { loading } = useBootstrapEmployee();

  if (isLoading || loading) {
    return <Loading message="Loading auth..." />;
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (!employee) {
    return <Loading message="Loading user..." />;
  }

  return <>{children}</>;
}
