import { Button, Paper, Stack, Box, Typography, ThemeProvider } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { codeApi } from "@/api/codeApi";
import type { Code } from "@/types/Code/Code";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { UpdateCodeRequest } from "@/types/Code/UpdateCodeRequest";
import CodeFormDialog from "@/pages/Codes/CodeFormDialog";
import { useMuiTheme } from "@/shared/hooks/useMuiTheme";

export default function CodesPage() {
    const muiTheme = useMuiTheme();

    const [rows, setRows] = useState<Code[]>([]);
    const [loading, setLoading] = useState(false);
    const [editCode, setEditCode] = useState<Code | null>(null);
    const [dialogOpen, setDialogOpen] = useState(false);

    const handleDelete = async (id: number) => {
        await codeApi.deleteCode(id);
        setRows((prev) => prev.filter((r) => r.id !== id));
    };

    const handleCreate = async (req: CreateCodeRequest) => {
        await codeApi.create(req);
        const data = await codeApi.getAll();
        setRows(data);
    };

    const handleUpdate = async (req: UpdateCodeRequest) => {
        await codeApi.update(req);
        setRows((prev) => prev.map((r) => r.id === req.id ? { ...r, ...req } : r));
    };

    const openCreate = () => {
        setEditCode(null);
        setDialogOpen(true);
    };

    const openEdit = (code: Code) => {
        setEditCode(code);
        setDialogOpen(true);
    };

    const columns: GridColDef<Code>[] = [
        { field: "id", headerName: "ID", width: 70 },
        { field: "display", headerName: "Code", width: 80 },
        { field: "totalEx", headerName: "Total Ex", flex: 1 },
        { field: "diffEx", headerName: "Diff Ex", flex: 1 },
        { field: "levelVisionRu", headerName: "Level Vision (RU)", flex: 2 },
        { field: "levelVisionEn", headerName: "Level Vision (EN)", flex: 2 },
        {
            field: "actions",
            headerName: "Actions",
            sortable: false,
            filterable: false,
            width: 160,
            align: "center",
            headerAlign: "center",
            renderCell: (params) => (
                <Stack direction="row" spacing={1} alignItems="center" height="100%">
                    <Button size="small" variant="contained" onClick={() => openEdit(params.row)}>
                        Edit
                    </Button>
                    <Button size="small" variant="outlined" color="error" onClick={() => handleDelete(params.row.id)}>
                        Delete
                    </Button>
                </Stack>
            ),
        },
    ];

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            const data = await codeApi.getAll();
            setRows(data);
            setLoading(false);
        };
        fetchData();
    }, []);

    return (
        <ThemeProvider theme={muiTheme}>
            <Box>
                <Stack direction="row" justifyContent="space-between" alignItems="center" mb={2}>
                    <Typography variant="h5">Codes</Typography>
                    <Button variant="contained" onClick={openCreate}>Create</Button>
                </Stack>

                <Paper sx={{ height: "calc(100vh - 300px)", width: "100%", display: "flex", flexDirection: "column" }}>
                    <Box sx={{ flex: 1 }}>
                        <DataGrid
                            rows={rows}
                            columns={columns}
                            loading={loading}
                            disableRowSelectionOnClick
                        />
                    </Box>
                </Paper>
            </Box>

            <CodeFormDialog
                open={dialogOpen}
                code={editCode}
                onClose={() => setDialogOpen(false)}
                onCreate={handleCreate}
                onUpdate={handleUpdate}
            />
        </ThemeProvider>
    );
}
