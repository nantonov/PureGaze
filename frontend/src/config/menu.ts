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
        path: "/AssessmentRequest"
    },
    {
        label: "Assessments",
        path: "/Assessment"
    },
    {
      label: "Employees",
      path: "/Employee",
      roles: ["admin"]
    },
  ];