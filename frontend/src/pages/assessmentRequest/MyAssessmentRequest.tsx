import { Box } from "@mui/material";
import { type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { assessmentRequestApi } from "@/shared/api/assessmentRequestApi.ts";
import type { AssessmentRequest } from "@/entities/assessmentRequest/AssessmentRequest.ts";
import type { GetAssessmentRequestsQuery } from "@/entities/assessmentRequest/GetAssessmentRequestsQuery.ts";
import {PaginationTable} from "@/shared/ui/table/pagination/PaginationTable.tsx";
import PageContentBox from "@/widgets/tableBox/PageContentBox.tsx";

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

type MyAssessmentRequestProps = {
    refreshTrigger?: number;
};

export default function MyAssessmentRequest({ refreshTrigger }: MyAssessmentRequestProps = {}) {
    const [rows, setRows] = useState<AssessmentRequest[]>([]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
        page: 0,
        pageSize: 10,
    });

    const handlePaginationChange = (model: GridPaginationModel) => {
        setPaginationModel((prev) => {
            return prev.pageSize !== model.pageSize 
                ? {...model, page: 0} 
                : model;
        });
    };
    
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
    }, [paginationModel, refreshTrigger]);
    
    return (
        <PageContentBox>
            <Box
                sx={{
                    flex: 1,
                    minHeight: 0,
                    width: "100%",
                    display: "flex",
                    flexDirection: "column",
                }}>
                <PaginationTable
                    rows={rows}
                    columns={columns}
                    rowCount={total}
                    loading={loading}
                    paginationMode="server"
                    paginationModel={paginationModel}
                    onPaginationModelChange={handlePaginationChange}
                    pageSizeOptions={[5, 10, 20, 50]}
                />
            </Box>
        </PageContentBox>
    );
}