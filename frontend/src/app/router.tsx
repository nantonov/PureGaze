import { createBrowserRouter } from "react-router-dom";
import MainLayout from "./layouts/MainLayout/MainLayout";
import Dashboard from "../pages/Dashboard/Dashboard";
import AssessmentRequest from "../pages/AssessmentRequest/AssessmentRequest";
import Assessment from "../pages/Assessment/Assessment";
import Employee from "../pages/Employee/Employee";

export const router = createBrowserRouter([
  {
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <Dashboard />,
      },
      {
        path: "AssessmentRequest",
        element: <AssessmentRequest />,
      },
      {
        path: "Assessment",
        element: <Assessment />,
      },
      {
        path: "Employee",
        element: <Employee />,
      },
    ],
  },
]);
