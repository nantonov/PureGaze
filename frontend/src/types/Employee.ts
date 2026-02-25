export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  managerLevelId?: string;
  role?: string;
  language?: string;
}
