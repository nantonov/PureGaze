import { employeeApi } from "@/shared/api/employeeApi";
import type { CurrentEmployee } from "@/entities/employees/CurrentEmployee.ts";

export async function loadCurrentEmployee(): Promise<CurrentEmployee> {
  return await employeeApi.getMe();
}
