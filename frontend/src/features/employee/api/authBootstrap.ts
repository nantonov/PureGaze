import { employeeApi } from "@/features/employee/api/employeeApi";
import type { CurrentEmployee } from "@/features/employee/model/CurrentEmployee.ts";

export async function loadCurrentEmployee(): Promise<CurrentEmployee> {
  return await employeeApi.getMe();
}
