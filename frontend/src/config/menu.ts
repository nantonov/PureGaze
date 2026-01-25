import type { MenuItem } from "@/types/MenuItem";
import { ROUTES } from "@/shared/constants";

export const menuItems: MenuItem[] = [
  {
    label: "Dashboard",
    path: ROUTES.DASHBOARD,
  },
  {
    label: "Assessment Requests",
    path: ROUTES.ASSESSMENT_REQUESTS,
  },
  {
    label: "Assessments",
    path: ROUTES.ASSESSMENTS,
  },
  {
    label: "Employees",
    path: ROUTES.EMPLOYEES,
    roles: ["admin"],
  },
];
