import type {Employee} from "@/features/employee/model/Employee.ts";

export interface GetEmployeesResponse {
    total: number;
    employees: Employee[];
}