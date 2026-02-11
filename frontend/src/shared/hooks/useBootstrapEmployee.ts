import { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { loadCurrentEmployee } from "@/services/auth/authBootstrap";
import { useEmployee } from "@/contexts/EmployeeContext";

export function useBootstrapEmployee() {
  const { isAuthenticated, isLoading } = useAuth0();
  const { employee, setEmployee } = useEmployee();

  const [loading, setLoading] = useState(false);

  useEffect(() => {
    console.log("bootstrap", { isLoading, isAuthenticated, employee });

    if (isLoading) return;
    if (!isAuthenticated) return;
    if (employee) return;

    setLoading(true);

    loadCurrentEmployee()
      .then(setEmployee)
      .catch(() => setEmployee(null))
      .finally(() => setLoading(false));
  }, [isAuthenticated, isLoading, employee, setEmployee]);

  return { loading };
}
