import type {Employee} from "@/entities/employees/Employee.ts";

export interface GetEmployeesResponse {
    total: number;
    employees: Employee[];
}