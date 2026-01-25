import { createBrowserRouter } from "react-router-dom";
import ProtectedRoute from "@/routes/ProtectedRoute";
import MainLayout from "@/layouts/MainLayout/MainLayout";
import Dashboard from "@/pages/Dashboard/Dashboard";
import AssessmentRequest from "@/pages/AssessmentRequest/AssessmentRequest";
import Assessment from "@/pages/Assessment/Assessment";
import Employee from "@/pages/Employee/Employee";
import Login from "@/pages/Login/Login";
import { ROUTES } from "@/shared/constants";

export const router = createBrowserRouter([
  {
    path: ROUTES.LOGIN,
    element: <Login />,
  },
  {
    element: (
      <ProtectedRoute>
        <MainLayout />
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
        element: <Employee />,
      },
    ],
  },
]);
