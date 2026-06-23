import type { MenuItem } from "@/shared/model/MenuItem";
import { ROUTES } from "@/shared/constants";
import { LuLayoutDashboard, LuNotebookPen, LuCode, LuUsers, LuFileText } from "react-icons/lu";
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
    label: "My Requests",
    path: ROUTES.ASSESSMENT_REQUESTS,
    icon: <LuNotebookPen size={25} />,
    namespace: "assessmentRequests",
  },
  {
    label: "Assessment Requests",
    path: ROUTES.ASSESSMENTS,
    icon: <PiVideoConference size={25} style={{ transform: "scale(1.15)" }} />,
    namespace: "assessments",
  },
  {
    label: "Assessments",
    path: ROUTES.CONDUCT_ASSESSMENTS,
    icon: <LuFileText size={25} />,
    namespace: "conductAssessments",
  },
  {
    label: "Management",
    namespace: "management",
    icon: <MdManageAccounts size={25} />,
    roles: ["M3", "M4", "M5"],
    children: [
      {
        label: "Employees",
        path: ROUTES.EMPLOYEES,
        icon: <LuUsers size={25} />,
        namespace: "employees",
      },
      {
        label: "Codes",
        path: ROUTES.CODES,
        icon: <LuCode size={25} />,
        namespace: "codes",
      },
      {
        label: "Assessment Templates",
        path: ROUTES.ASSESSMENT_TEMPLATES,
        icon: <LuFileText size={25} />,
        namespace: "assessmentTemplates",
      },
    ],
  },
];
