import { Paper, Box, Typography } from "@mui/material";
import { DataGrid, type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import type { Employee } from "@/types/Employees/Employee.ts";
import {employeeApi} from "@/api/employeeApi.ts";
import type {GetEmployeesQuery} from "@/types/Employees/GetEmployeesQuery.ts";

const columns: GridColDef<Employee>[] = [
    { field: "id", headerName: "ID", width: 90 },
    {
        field: "fullName",
        headerName: "Name",
        flex: 1,
    },
    {
        field: "email",
        headerName: "Email",
        flex: 1,
    },
    {
        field: "managerLevel",
        headerName: "M-Level",
        flex: 1,
    },
    {
        field: "m1",
        headerName: "M1",
        flex: 1,
    },
    {
        field: "m2",
        headerName: "M2",
        flex: 1,
    },
    {
        field: "m3",
        headerName: "M3",
        flex: 1,
    },
];

export default function Employee() {
  const { t } = useTranslation("employees");
  const [rows, setRows] = useState<Employee[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
      page: 0, 
      pageSize: 10,
  });
  
  useEffect(() => {
      const fetchData = async () => {
          setLoading(true);

          const query: GetEmployeesQuery = {
              page: paginationModel.page + 1,
              pageSize: paginationModel.pageSize,
          };
          
          const response = await  employeeApi.getEmployees(query);

          setRows(response?.employees ?? []);
          setTotal(response?.total ?? 0);
          
          setLoading(false);
      };
      
      fetchData();
  }, [paginationModel]);
  
  return (
      <Box  sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
        <Typography variant="h4">
          {t("title")}      
        </Typography>
        
        <Box>
              <Paper sx={{
                  height: "calc(100vh - 300px)",
                  width: "100%",
                  display: "flex",
                  flexDirection: "column" }}>
                  <Box sx={{ flex: 1 }}>
                      <DataGrid
                          rows={rows}
                          columns={columns}
                          rowCount={total}
                          loading={loading}
                          paginationMode="server"
                          paginationModel={paginationModel}
                          onPaginationModelChange={setPaginationModel}
                          pageSizeOptions={[5, 10, 20, 50]}
                          disableRowSelectionOnClick
                      />
                  </Box>
              </Paper>
        </Box>
      </Box>
  );
}
