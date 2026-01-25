import { employeeApi } from "@/api/employeeApi";
import type { Employee } from "@/types/Employee";

export async function loadCurrentEmployee(): Promise<Employee> {
  return await employeeApi.getMe();
}
