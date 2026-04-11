import { BaseApi } from "./baseApi";
import type {GetEmployeesQuery} from "@/entities/employees/GetEmployeesQuery.ts";
import type {GetEmployeesResponse} from "@/entities/employees/GetEmployeesResponse.ts";

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