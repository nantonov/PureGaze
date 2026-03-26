import { BaseApi } from "./baseApi";
import type { CurrentEmployee } from "@/types/Employees/CurrentEmployee.ts";
import type {GetEmployeesQuery} from "@/types/Employees/GetEmployeesQuery.ts";
import type {GetEmployeesResponse} from "@/types/Employees/GetEmployeesResponse.ts";

class EmployeeApi extends BaseApi {
  private readonly baseUrl = "/employees";

  public async getMe(): Promise<CurrentEmployee> {
    return this.get<CurrentEmployee>(`${this.baseUrl}/me`);
  }

  public async updateLanguage(language: string): Promise<void> {
    return this.put<void>(`${this.baseUrl}/language`, { language });
  }
  
  public async getEmployees(query: GetEmployeesQuery): Promise<GetEmployeesResponse> {
    return this.post<GetEmployeesResponse>(`${this.baseUrl}`, query);
  }
}

export const employeeApi = new EmployeeApi();
