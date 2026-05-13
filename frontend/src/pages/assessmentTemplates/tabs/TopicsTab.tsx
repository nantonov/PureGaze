import { Box, Button, IconButton, InputAdornment, Stack, TextField, Tooltip } from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { Pencil, Plus, Search, Trash2 } from "lucide-react";
import { useMemo, useState } from "react";
import type { Topic } from "@/entities/template/Topic.ts";
import { ConfirmDeleteDialog } from "@/shared/ui/ConfirmDeleteDialog.tsx";

export interface TopicRow {
  id: number;
  name: string;
  subtopicCount: number;
}

interface Props {
  rows: TopicRow[];
  loading: boolean;
  onAdd: () => void;
  onEdit: (topic: Topic) => void;
  onDelete: (id: number) => void;
  onSelect: (topicId: number) => void;
}

export default function TopicsTab({ rows, loading, onAdd, onEdit, onDelete, onSelect }: Props) {
  const [query, setQuery] = useState("");
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const filtered = useMemo(() => {
    const q = query.trim().toLowerCase();
    if (!q) return rows;
    return rows.filter((r) => r.name.toLowerCase().includes(q));
  }, [rows, query]);

  const columns: GridColDef<TopicRow>[] = [
    { field: "name", headerName: "Topic", flex: 1, minWidth: 200 },
    {
      field: "subtopicCount",
      headerName: "Subtopics",
      width: 130,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => (
        <Box
          sx={{
            display: "inline-flex",
            alignItems: "center",
            justifyContent: "center",
            minWidth: 32,
            height: 22,
            px: 1,
            borderRadius: "11px",
            fontSize: 12,
            fontWeight: 600,
            bgcolor: params.value > 0 ? "rgba(198, 48, 49, 0.1)" : "action.hover",
            color: params.value > 0 ? "var(--brand-color)" : "text.secondary",
          }}
        >
          {params.value}
        </Box>
      ),
    },
    {
      field: "actions",
      headerName: "Actions",
      width: 120,
      sortable: false,
      filterable: false,
      align: "center",
      headerAlign: "center",
      renderCell: (params) => (
        <Stack direction="row" spacing={0.5} alignItems="center" height="100%">
          <Tooltip title="Edit">
            <IconButton
              size="small"
              onClick={(e) => {
                e.stopPropagation();
                onEdit({ id: params.row.id, name: params.row.name });
              }}
            >
              <Pencil size={16} />
            </IconButton>
          </Tooltip>
          <Tooltip title="Delete">
            <IconButton
              size="small"
              color="error"
              onClick={(e) => {
                e.stopPropagation();
                setDeleteId(params.row.id);
              }}
            >
              <Trash2 size={16} />
            </IconButton>
          </Tooltip>
        </Stack>
      ),
    },
  ];

  return (
    <Stack sx={{ flex: 1, minHeight: 0 }} spacing={2}>
      <Stack direction="row" spacing={1.5} alignItems="center">
        <TextField
          size="small"
          placeholder="Search topics…"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          sx={{ flex: 1, maxWidth: 360 }}
          slotProps={{
            input: {
              startAdornment: (
                <InputAdornment position="start">
                  <Search size={16} />
                </InputAdornment>
              ),
            },
          }}
        />
        <Box sx={{ flex: 1 }} />
        <Button
          variant="contained"
          startIcon={<Plus size={16} />}
          onClick={onAdd}
          sx={{
            bgcolor: "var(--brand-color)",
            "&:hover": { bgcolor: "var(--brand-color)", filter: "brightness(0.92)" },
            textTransform: "none",
          }}
        >
          Add topic
        </Button>
      </Stack>

      <Box sx={{ flex: 1, minHeight: 0 }}>
        <DataGrid
          rows={filtered}
          columns={columns}
          loading={loading}
          disableRowSelectionOnClick
          density="compact"
          hideFooter
          onRowClick={(params) => onSelect(params.row.id)}
          sx={{
            border: "1px solid var(--border-color, rgba(0,0,0,0.12))",
            borderRadius: 2,
            "& .MuiDataGrid-columnHeaders": { bgcolor: "action.hover" },
            "& .MuiDataGrid-row": { cursor: "pointer" },
          }}
        />
      </Box>

      <ConfirmDeleteDialog
        open={deleteId !== null}
        label="Topic"
        onCancel={() => setDeleteId(null)}
        onConfirm={() => {
          if (deleteId !== null) onDelete(deleteId);
          setDeleteId(null);
        }}
      />
    </Stack>
  );
}
