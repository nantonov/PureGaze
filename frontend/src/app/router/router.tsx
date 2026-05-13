import { createBrowserRouter, Outlet } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import MainLayout from "@/widgets/layouts/main/MainLayout.tsx";
import Dashboard from "@/pages/dashboard/Dashboard";
import AssessmentRequest from "@/pages/assessmentRequest/AssessmentRequest";
import Assessment from "@/pages/assessment/Assessment";
import Employee from "@/pages/employee/Employee";
import Login from "@/pages/login/Login";
import { ROUTES } from "@/shared/constants";
import RoleProtectedRoute from "@/app/router/RoleProtectdRoute";
import CodesPage from "@/pages/codes/CodesPage";
import AssessmentTemplatesPage from "@/pages/assessmentTemplates/AssessmentTemplatesPage";

export const router = createBrowserRouter([
  {
    path: ROUTES.LOGIN,
    element: <Login />,
  },
  {
    element: <MainLayout />,
    children: [
      {
        element: (
          <ProtectedRoute>
            <Outlet />
          </ProtectedRoute>
        ),
        children: [
          {
            index: true,
            element: <Dashboard />,
          },
          {
            path: "assessment-requests",
            element: <AssessmentRequest />,
          },
          {
            path: "assessments",
            element: <Assessment />,
          },
          {
            path: "employees",
            element: (
              <RoleProtectedRoute roles={["M5", "M4", "M3"]}>
                <Employee />
              </RoleProtectedRoute>
            ),
          },
          {
            path: "codes",
            element: (
              <RoleProtectedRoute roles={["M3", "M4", "M5"]}>
                <CodesPage />
              </RoleProtectedRoute>
            ),
          },
          {
            path: "assessment-templates",
            element: (
              <RoleProtectedRoute roles={["M3", "M4", "M5"]}>
                <AssessmentTemplatesPage />
              </RoleProtectedRoute>
            ),
          },
        ],
      },
    ],
  },
]);
