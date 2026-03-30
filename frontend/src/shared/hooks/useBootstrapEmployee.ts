import { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { loadCurrentEmployee } from "@/services/auth/authBootstrap";
import { useEmployee } from "@/contexts/EmployeeContext";

export function useBootstrapEmployee() {
  const { isAuthenticated, isLoading } = useAuth0();
  const { employee, setEmployee } = useEmployee();

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {

    if (isLoading) return;

    if (!isAuthenticated) {
      setLoading(false);
      return;
    }

    if (employee) {
      setLoading(false);
      return;
    }
    
    setLoading(true);

    loadCurrentEmployee()
      .then(setEmployee)
      .catch(() => setError(true))
      .finally(() => setLoading(false));
  }, [isAuthenticated, isLoading, employee, setEmployee]);

  return { loading, error };
}
