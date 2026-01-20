export type MenuItem = {
  label: string;
  path: string;
  roles?: string[];
};

export const menuItems: MenuItem[] = [
  {
    label: "Dashboard",
    path: "/",
  },
  {
    label: "Assessment Requests",
    path: "/assessment-requests",
  },
  {
    label: "Assessments",
    path: "/assessments",
  },
  {
    label: "Employees",
    path: "/employees",
    roles: ["admin"],
  },
];
