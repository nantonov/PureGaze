import type {Employee} from "@/types/Employees/Employee.ts";

export interface GetEmployeesResponse {
    total: number;
    employees: Employee[];
}