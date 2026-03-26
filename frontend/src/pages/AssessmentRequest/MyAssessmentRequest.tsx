import { Paper, Button, Box } from "@mui/material";
import { DataGrid, type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { assessmentRequestApi } from "@/api/assessmentRequestApi.ts";
import type { AssessmentRequest } from "@/types/AssessmentRequest/AssessmentRequest.ts";
import type { GetAssessmentRequestsQuery } from "@/types/AssessmentRequest/GetAssessmentRequestsQuery.ts";

const columns: GridColDef<AssessmentRequest>[] = [
    { field: "id", headerName: "ID", width: 90 },
    {
        field: "managerFullName",
        headerName: "To",
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
];

export default function MyAssessmentRequest(){
    const [rows, setRows] = useState<AssessmentRequest[]>([]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0,
        pageSize: 10,
    });

    const fetchData = async () => {
        setLoading(true);

        const request: GetAssessmentRequestsQuery = {
            page: paginationModel.page + 1,
            pageSize: paginationModel.pageSize,
        };

        const response = await assessmentRequestApi.getMyRequests(request);

        setRows(response?.assessmentRequests ?? []);
        setTotal(response?.total ?? 0);

        setLoading(false);
    };
    
    useEffect(() => {
        fetchData();
    }, [paginationModel]);

    const handleCreteAssessmentRequest = async () => {
        await assessmentRequestApi.CreateAssessmentRequest();
        
        await fetchData();
    };
    
    return (
        <Paper sx={{ 
            height: "calc(100vh - 300px)", 
            width: "100%", 
            display: "flex", 
            flexDirection: "column" }}>
            <Box sx={{
                py: 1,
                display: "flex",
                justifyContent: "flex-end",}}>
                <Button
                    variant="contained"
                    color="success"
                    onClick={handleCreteAssessmentRequest}>
                    New Request</Button>
            </Box>
            <Box sx={{ flex: 1 }}>
                <DataGrid
                    rows={rows}
                    columns={columns}
                    rowCount={total}
                    loading={loading}
                    paginationMode="server"
                    paginationModel={paginationModel}
                    onPaginationModelChange={setPaginationModel}
                    pageSizeOptions={[10, 20, 50]}
                    disableRowSelectionOnClick
                />
            </Box>
        </Paper>
    );
}