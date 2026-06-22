import {
  Box,
  Button,
  Chip,
  FormControl,
  IconButton,
  InputAdornment,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { Pencil, Plus, Search, Trash2 } from "lucide-react";
import { useMemo, useState } from "react";
import { ConfirmDeleteDialog } from "@/shared/ui/ConfirmDeleteDialog.tsx";

export interface SubtopicRow {
  id: number;
  name: string;
  topicId: number;
  topicName: string;
  questionCount: number | null;
}

interface Props {
  rows: SubtopicRow[];
  topics: { id: number; name: string }[];
  loading: boolean;
  topicFilter: number | "";
  onTopicFilterChange: (v: number | "") => void;
  onAdd: () => void;
  onEdit: (subtopicId: number) => void;
  onDelete: (id: number) => void;
  onSelect: (subtopicId: number) => void;
}

export default function SubtopicsTab({
  rows,
  topics,
  loading,
  topicFilter,
  onTopicFilterChange,
  onAdd,
  onEdit,
  onDelete,
  onSelect,
}: Props) {
  const [query, setQuery] = useState("");
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const filtered = useMemo(() => {
    const q = query.trim().toLowerCase();
    return rows.filter((r) => {
      const matchesQuery = !q || r.name.toLowerCase().includes(q);
      const matchesTopic = topicFilter === "" || r.topicId === topicFilter;
      return matchesQuery && matchesTopic;
    });
  }, [rows, query, topicFilter]);

  const columns: GridColDef<SubtopicRow>[] = [
    { field: "name", headerName: "Subtopic", flex: 1, minWidth: 200 },
    {
      field: "topicName",
      headerName: "Parent topic",
      flex: 1,
      minWidth: 180,
      renderCell: (params) => (
        <Chip
          label={params.value}
          size="small"
          sx={{
            bgcolor: "rgba(198, 48, 49, 0.08)",
            color: "var(--brand-color)",
            fontWeight: 500,
          }}
        />
      ),
    },
    {
      field: "questionCount",
      headerName: "Questions",
      width: 100,
      align: "center",
      headerAlign: "center",
      sortable: false,
      renderCell: (params) => {
        const count = params.value as number | null;
        if (count === null)
          return (
            <Typography variant="body2" color="text.secondary">
              –
            </Typography>
          );
        return (
          <Chip
            label={count}
            size="small"
            sx={
              count > 0
                ? {
                    bgcolor: "rgba(198, 48, 49, 0.08)",
                    color: "var(--brand-color)",
                    fontWeight: 600,
                  }
                : { bgcolor: "action.hover", fontWeight: 500 }
            }
          />
        );
      },
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
                onEdit(params.row.id);
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
      <Stack direction="row" spacing={1.5} alignItems="center" flexWrap="wrap">
        <TextField
          size="small"
          placeholder="Search subtopics…"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          sx={{ flex: 1, maxWidth: 320, minWidth: 200 }}
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
        <FormControl size="small" sx={{ minWidth: 200 }}>
          <InputLabel>Filter by topic</InputLabel>
          <Select
            value={topicFilter}
            label="Filter by topic"
            onChange={(e) => onTopicFilterChange(e.target.value as number | "")}
          >
            <MenuItem value="">All topics</MenuItem>
            {topics.map((t) => (
              <MenuItem key={t.id} value={t.id}>
                {t.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <Box sx={{ flex: 1 }} />
        <Button
          variant="contained"
          startIcon={<Plus size={16} />}
          onClick={onAdd}
          disabled={topics.length === 0}
          sx={{
            bgcolor: "var(--brand-color)",
            "&:hover": { bgcolor: "var(--brand-color)", filter: "brightness(0.92)" },
            textTransform: "none",
          }}
        >
          Add subtopic
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
        label="Subtopic"
        onCancel={() => setDeleteId(null)}
        onConfirm={() => {
          if (deleteId !== null) onDelete(deleteId);
          setDeleteId(null);
        }}
      />
    </Stack>
  );
}
