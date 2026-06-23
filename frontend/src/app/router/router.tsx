import { createBrowserRouter, Outlet } from "react-router-dom";
import ProtectedRoute from "./ProtectedRoute";
import MainLayout from "@/widgets/layouts/main/MainLayout.tsx";
import Dashboard from "@/features/dashboard/page/Dashboard";
import AssessmentRequest from "@/features/assessment-request/page/AssessmentRequest";
import Assessment from "@/features/assessment/page/Assessment";
import ConductAssessments from "@/features/assessment/page/ConductAssessments";
import Employee from "@/features/employee/page/Employee";
import Login from "@/features/auth/page/Login";
import { ROUTES } from "@/shared/constants";
import RoleProtectedRoute from "@/app/router/RoleProtectdRoute";
import CodesPage from "@/features/code/page/CodesPage";
import AssessmentTemplatesPage from "@/features/assessment-template/page/AssessmentTemplatesPage";

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
            path: "conduct-assessments",
            element: <ConductAssessments />,
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
