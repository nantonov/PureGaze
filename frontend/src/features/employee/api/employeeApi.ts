import { BaseApi } from "@/shared/api/baseApi";
import type { CurrentEmployee } from "@/features/employee/model/CurrentEmployee.ts";

class EmployeeApi extends BaseApi {
  private readonly baseUrl = "/employees";

  public async getMe(): Promise<CurrentEmployee> {
    return this.get<CurrentEmployee>(`${this.baseUrl}/me`);
  }

  public async updateLanguage(language: string): Promise<void> {
    return this.put<void>(`${this.baseUrl}/language`, { language });
  }
}

export const employeeApi = new EmployeeApi();
