import { BaseApi } from "./baseApi";
import type { Employee } from "@/types/Employee";

class EmployeeApi extends BaseApi {
  private readonly baseUrl = "/employees";

  public async getMe(): Promise<Employee> {
    return this.get<Employee>(`${this.baseUrl}/me`);
  }
}

export const employeeApi = new EmployeeApi();
