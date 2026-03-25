import { employeeApi } from "@/api/employeeApi";
import type { CurrentEmployee } from "@/types/CurrentEmployee.ts";

export async function loadCurrentEmployee(): Promise<CurrentEmployee> {
  return await employeeApi.getMe();
}
