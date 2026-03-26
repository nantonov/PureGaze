import type { MenuItem } from "@/types/MenuItem";
import { ROUTES } from "@/shared/constants";
import { LuLayoutDashboard, LuNotebookPen, LuCode, LuUsers } from "react-icons/lu";
import { PiVideoConference } from "react-icons/pi";
import { MdManageAccounts } from "react-icons/md";

export const menuItems: MenuItem[] = [
  {
    label: "Dashboard",
    path: ROUTES.DASHBOARD,
    icon: <LuLayoutDashboard size={25} />,
    namespace: "dashboard",
  },
  {
    label: "Assessment Requests",
    path: ROUTES.ASSESSMENT_REQUESTS,
    icon: <LuNotebookPen size={25} />,
    namespace: "assessmentRequests",
  },
  {
    label: "Assessments",
    path: ROUTES.ASSESSMENTS,
    icon: <PiVideoConference size={25} style={{ transform: "scale(1.15)" }} />,
    namespace: "assessments",
  },
  {
    label: "Management",
    namespace: "management",
    icon: <MdManageAccounts size={25}/>,
    roles: ["M3", "M4", "M5"],
    children: [
      {
        label: "Employees",
        path: ROUTES.EMPLOYEES,
        icon: <LuUsers size={25}/>,
        namespace: "employees",
      },
      {
        label: "Codes",
        path: ROUTES.CODES,
        icon: <LuCode size={25}/>,
        namespace: "codes",
      },
    ]
  }
];
