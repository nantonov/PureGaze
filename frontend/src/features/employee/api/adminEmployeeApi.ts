import { BaseApi } from "@/shared/api/baseApi";
import type {GetEmployeesQuery} from "@/features/employee/model/GetEmployeesQuery.ts";
import type {GetEmployeesResponse} from "@/features/employee/model/GetEmployeesResponse.ts";

class AdminEmployeeApi extends BaseApi {
  private readonly baseUrl = "/admin-employees";
  
  public async getEmployees(query: GetEmployeesQuery): Promise<GetEmployeesResponse> {
    return this.post<GetEmployeesResponse>(`${this.baseUrl}`, query);
  }

  public async syncEmployees(): Promise<void> {
    return this.post(`${this.baseUrl}/sync`);
  }
}

export const adminEmployeeApi = new AdminEmployeeApi();