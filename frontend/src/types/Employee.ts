export interface Employee {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role?: "admin"|"employee"
};