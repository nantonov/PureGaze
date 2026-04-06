import {Button, Paper, Stack, Box } from "@mui/material";
import { DataGrid, type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { assessmentRequestApi } from "@/shared/api/assessmentRequestApi";
import type { GetAssessmentRequestsQuery } from "@/entities/assessmentRequest/GetAssessmentRequestsQuery.ts";
import type { AssessmentRequest } from "@/entities/assessmentRequest/AssessmentRequest.ts";

const columns: GridColDef<AssessmentRequest>[] = [
    { field: "id", headerName: "ID", width: 90 },
    {
        field: "employeeFullName",
        headerName: "From",
        flex: 1,
    },
    {
        field: "code",
        headerName: "Code",
        flex: 1,
    },
    {
        field: "status",
        headerName: "Status",
        flex: 1,
    },
    {
        field: "actions",
        headerName: "Actions",
        sortable: false,
        filterable: false,
        width: 200,
        renderCell: (params) => {
            const { row } = params;
            
            const handleApprove = async () => {
                console.log("Approve", row.id);
            };

            const handleReject = async () => {
                console.log("Reject", row.id);
            };
            
            if (row.status !== "Created") {
                return null;
            }

            return (
                <Stack direction="row" spacing={1}>
                    <Button
                        size="small"
                        variant="contained"
                        color="success"
                        onClick={()=> handleApprove}
                    >
                        Approve
                    </Button>

                    <Button
                        size="small"
                        variant="outlined"
                        color="error"
                        onClick={()=> handleReject}
                    >
                        Reject
                    </Button>
                </Stack>
            );
        }
    },
];
export default function AssessmentRequestToMe() {
    const [rows, setRows] = useState<AssessmentRequest[]>([]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0,
        pageSize: 10,
    });

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);

            const query: GetAssessmentRequestsQuery = {
                page: paginationModel.page + 1,
                pageSize: paginationModel.pageSize,
            };

            const response = await assessmentRequestApi.getAssignedToMe(query);

            setRows(response?.assessmentRequests ?? []);
            setTotal(response?.total ?? 0);

            setLoading(false);
        };

        fetchData();
    }, [paginationModel]);

    return (
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
    );
}