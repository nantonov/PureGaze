import { Button, Box } from "@mui/material";
import { type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { useEffect, useState, useCallback, useMemo } from "react";
import { assessmentRequestApi } from "@/shared/api/assessmentRequestApi";
import type { GetAssessmentRequestsQuery } from "@/entities/assessmentRequest/GetAssessmentRequestsQuery.ts";
import type { AssessmentRequest } from "@/entities/assessmentRequest/AssessmentRequest.ts";
import { PaginationTable } from "@/shared/ui/table/pagination/PaginationTable.tsx";
import PageContentBox from "@/widgets/tableBox/PageContentBox.tsx";
import { AssessmentRequestActionDialog } from "./AssessmentRequestActionDialog.tsx";

type Action = "approve" | "reject";

type DialogState = {
    open: boolean;
    action: Action | null;
    row: AssessmentRequest | null;
};

const closedDialog: DialogState = { open: false, action: null, row: null };

export default function AssessmentRequestToMe() {
    const [rows, setRows] = useState<AssessmentRequest[]>([]);
    const [total, setTotal] = useState(0);
    const [loading, setLoading] = useState(false);
    const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({ page: 0, pageSize: 10 });
    const [dialog, setDialog] = useState<DialogState>(closedDialog);

    const fetchData = useCallback(async () => {
        setLoading(true);
        const query: GetAssessmentRequestsQuery = {
            page: paginationModel.page + 1,
            pageSize: paginationModel.pageSize,
        };
        const response = await assessmentRequestApi.getAssignedToMe(query);
        setRows(response?.assessmentRequests ?? []);
        setTotal(response?.total ?? 0);
        setLoading(false);
    }, [paginationModel]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    const closeDialog = () => setDialog(prev => ({ ...prev, open: false }));

    const handleConfirm = async (reason?: string) => {
        if (!dialog.row || !dialog.action) return;
        if (dialog.action === "approve") {
            await assessmentRequestApi.approve(dialog.row.id);
        } else {
            await assessmentRequestApi.reject(dialog.row.id, reason);
        }
        closeDialog();
        await fetchData();
    };

    const columns = useMemo<GridColDef<AssessmentRequest>[]>(() => [
        { field: "id", headerName: "ID", width: 90 },
        { field: "employeeFullName", headerName: "From", flex: 1 },
        { field: "code", headerName: "Code", flex: 1 },
        { field: "status", headerName: "Status", flex: 1 },
        {
            field: "actions",
            headerName: "Actions",
            sortable: false,
            filterable: false,
            width: 200,
            headerAlign: "center",
            renderCell: (params) => {
                const { row } = params;

                if (row.status !== "Created") return null;

                return (
                    <Box sx={{ display: "flex", width: "100%", height: "100%", alignItems: "center", justifyContent: "center", gap: 1 }}>
                        <Button
                            size="small"
                            variant="contained"
                            color="success"
                            onClick={(e) => { e.stopPropagation(); setDialog({ open: true, action: "approve", row }); }}
                        >
                            Approve
                        </Button>
                        <Button
                            size="small"
                            variant="outlined"
                            color="error"
                            onClick={(e) => { e.stopPropagation(); setDialog({ open: true, action: "reject", row }); }}
                        >
                            Reject
                        </Button>
                    </Box>
                );
            },
        },
    ], [setDialog]);

    return (
        <PageContentBox>
            <Box sx={{ flex: 1, minHeight: 0, width: "100%", display: "flex", flexDirection: "column" }}>
                <PaginationTable
                    rows={rows}
                    columns={columns}
                    rowCount={total}
                    loading={loading}
                    paginationMode="server"
                    paginationModel={paginationModel}
                    onPaginationModelChange={setPaginationModel}
                    pageSizeOptions={[5, 10, 20, 50]}
                />
            </Box>

            <AssessmentRequestActionDialog
                open={dialog.open}
                action={dialog.action}
                code={dialog.row?.code ?? ""}
                employeeFullName={dialog.row?.employeeFullName ?? ""}
                onConfirm={handleConfirm}
                onClose={closeDialog}
            />
        </PageContentBox>
    );
}
