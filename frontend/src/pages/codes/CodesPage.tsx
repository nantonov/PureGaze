import { Button, Stack, Box, Typography } from "@mui/material";
import { type GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { codeApi } from "@/shared/api/adminCodeApi.ts";
import type { Code } from "@/entities/code/Code.ts";
import type { CreateCodeRequest } from "@/entities/code/CreateCodeRequest.ts";
import type { UpdateCodeRequest } from "@/entities/code/UpdateCodeRequest.ts";
import type {GetCode} from "@/entities/code/GetCode.ts";
import CodeFormDialog from "@/pages/codes/CodeFormDialog";
import PageContentBox from "@/widgets/tableBox/PageContentBox.tsx";
import {SimpleTable} from "@/shared/ui/table/SimpleTable.tsx";

export default function CodesPage() {
    const { t } = useTranslation("codes");
    const [rows, setRows] = useState<GetCode[]>([]);
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

    const openEdit = async (id: number) => 
    {
        const code = await codeApi.getById(id);
        setEditCode(code);
        setDialogOpen(true);
    };
    
    const openCreate = () => {
        setEditCode(null);
        setDialogOpen(true);
    };

    const columns: GridColDef<GetCode>[] = [
        { field: "id", headerName: "ID", flex: 1 },
        { field: "name", headerName: "Name", flex: 1 },
        { field: "display", headerName: "Display", flex: 1 },
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
                    <Button size="small" variant="contained" onClick={() => openEdit(params.row.id)}>
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
        <PageContentBox>
                <Stack 
                    direction="row" 
                    justifyContent="space-between" 
                    alignItems="center"
                    paddingTop={1}
                    sx={{ flexShrink: 0, width: "100%", minWidth: 0, boxSizing: "border-box" }}>
                    <Typography variant="h4" sx={{ minWidth: 0 }}>
                        {t("title")}
                    </Typography>
                    <Stack direction="row" spacing={2} alignItems="center" sx={{ flexShrink: 0 }}>
                        <Button variant="contained" onClick={openCreate}>Create</Button>
                    </Stack>
                </Stack>

            <Box
                sx={{
                    flex: 1,
                    minHeight: 0,
                    width: "100%",
                    display: "flex",
                    flexDirection: "column",
                }}
            >
                <SimpleTable
                    rows={rows}
                    columns={columns}
                    loading={loading}
                />
            </Box>
            
            <CodeFormDialog
                open={dialogOpen}
                code={editCode}
                onClose={() => setDialogOpen(false)}
                onCreate={handleCreate}
                onUpdate={handleUpdate}
            />
        </PageContentBox>
    );
}
