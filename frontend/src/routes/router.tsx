import {createBrowserRouter, Outlet} from "react-router-dom";
import ProtectedRoute from "@/routes/ProtectedRoute";
import MainLayout from "@/layouts/MainLayout/MainLayout";
import Dashboard from "@/pages/Dashboard/Dashboard";
import AssessmentRequest from "@/pages/AssessmentRequest/AssessmentRequest";
import Assessment from "@/pages/Assessment/Assessment";
import Employee from "@/pages/Employee/Employee";
import Login from "@/pages/Login/Login";
import {ROUTES} from "@/shared/constants";
import RoleProtectedRoute from "@/routes/RoleProtectdRoute.tsx";
import CodesPage from "@/pages/Codes/CodesPage";

export const router = createBrowserRouter([
    {
        path: ROUTES.LOGIN,
        element: <Login/>,
    },
    {
        element: <MainLayout/>,
        children: [
            {
                element: (
                    <ProtectedRoute>
                        <Outlet/>
                    </ProtectedRoute>
                ),
                children: [
                    {
                        index: true,
                        element: <Dashboard/>,
                    },
                    {
                        path: "assessment-requests",
                        element: <AssessmentRequest/>,
                    },
                    {
                        path: "assessments",
                        element: <Assessment/>,
                    },
                    {
                        path: "employees",
                        element: (
                            <RoleProtectedRoute roles={['M5', 'M4', 'M3']}>
                                <Employee/>
                            </RoleProtectedRoute>
                        ),
                    },
                    {
                        path: "codes",
                        element: (
                            <RoleProtectedRoute roles={['M3', 'M4', 'M5']}>
                                <CodesPage/>
                            </RoleProtectedRoute>
                        ),
                    },
                ],
            },
        ],
    },
]);
