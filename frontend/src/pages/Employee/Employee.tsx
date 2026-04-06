import { Paper, Box, Typography, TextField, ThemeProvider, Stack, Button } from "@mui/material";
import { DataGrid, type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { adminEmployeeApi } from "@/shared/api/adminEmployeeApi.ts";
import { useMuiTheme } from "@/shared/hooks/useMuiTheme.ts";
import type { Employee } from "@/entities/employees/Employee.ts";
import type { GetEmployeesQuery } from "@/entities/employees/GetEmployeesQuery.ts";

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
    const muiTheme = useMuiTheme();
    const { t } = useTranslation("employees");
    const [rows, setRows] = useState<Employee[]>([]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [search, setSearch] = useState("");
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0, 
        pageSize: 10,
    });
    
    const syncEmployees = async () => {
        setLoading(true);
        try {
            await adminEmployeeApi.syncEmployees();   
        }
        finally {
            setLoading(false);   
        }
    }

    const getEmployees = async () => {
        setLoading(true);
        try {
            const query: GetEmployeesQuery = {
                page: paginationModel.page + 1,
                pageSize: paginationModel.pageSize,
                search: search
            };

            const response = await adminEmployeeApi.getEmployees(query);

            setRows(response?.employees ?? []);
            setTotal(response?.total ?? 0);
        }
        finally {
            setLoading(false);
        }
    };
    
    useEffect(() => {
        const timeout = setTimeout(() => {
            getEmployees();
        }, 500);
        
        return () => clearTimeout(timeout);
    }, [search]);
    
    useEffect(() => { getEmployees(); }, [paginationModel]);
    
    return (
        <ThemeProvider theme={muiTheme}>
            <Box sx={{ display: "flex", flexDirection: "column", gap: 2 }}>
                <Stack direction="row" justifyContent="space-between" alignItems="center">
                    <Typography variant="h4">
                        {t("title")}
                    </Typography>
                    <Stack direction="row" spacing={2} alignItems="center">
                        <TextField
                            size="small"
                            label={t("Search")}
                            value={search}
                            onChange={(e) => setSearch(e.target.value)}
                        />
                        <Button
                            variant="contained"
                            disabled={loading}
                            onClick={() => syncEmployees()}>Sync</Button>
                    </Stack>
                </Stack>
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
        </ThemeProvider>
    );
}
