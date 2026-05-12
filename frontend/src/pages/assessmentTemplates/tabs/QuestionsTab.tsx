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
} from "@mui/material";
import { DataGrid, type GridColDef } from "@mui/x-data-grid";
import { Pencil, Plus, Search, Trash2 } from "lucide-react";
import { useMemo, useState } from "react";
import { ConfirmDeleteDialog } from "@/shared/ui/ConfirmDeleteDialog.tsx";

export interface QuestionRow {
  id: number;
  content: string;
  subtopicId: number;
  subtopicName: string;
  topicId: number;
  topicName: string;
}

interface Props {
  rows: QuestionRow[];
  topics: { id: number; name: string }[];
  subtopics: { id: number; name: string; topicId: number; topicName: string }[];
  loading: boolean;
  topicFilter: number | "";
  onTopicFilterChange: (v: number | "") => void;
  subtopicFilter: number | "";
  onSubtopicFilterChange: (v: number | "") => void;
  onRowClick: (questionId: number) => void;
  onAdd: () => void;
  onEdit: (questionId: number) => void;
  onDelete: (questionId: number) => void;
}

export default function QuestionsTab({
  rows,
  topics,
  subtopics,
  loading,
  topicFilter,
  onTopicFilterChange,
  subtopicFilter,
  onSubtopicFilterChange,
  onRowClick,
  onAdd,
  onEdit,
  onDelete,
}: Props) {
  const [query, setQuery] = useState("");
  const [deleteId, setDeleteId] = useState<number | null>(null);

  const visibleSubtopics = useMemo(
    () => (topicFilter === "" ? subtopics : subtopics.filter((s) => s.topicId === topicFilter)),
    [subtopics, topicFilter],
  );

  const handleTopicFilterChange = (v: number | "") => {
    onTopicFilterChange(v);
    onSubtopicFilterChange("");
  };

  const filtered = useMemo(() => {
    const q = query.trim().toLowerCase();
    return rows.filter((r) => {
      const matchesQuery = !q || r.content.toLowerCase().includes(q);
      const matchesTopic = topicFilter === "" || r.topicId === topicFilter;
      const matchesSubtopic = subtopicFilter === "" || r.subtopicId === subtopicFilter;
      return matchesQuery && matchesTopic && matchesSubtopic;
    });
  }, [rows, query, topicFilter, subtopicFilter]);

  const columns: GridColDef<QuestionRow>[] = [
    {
      field: "content",
      headerName: "Question",
      flex: 2,
      minWidth: 240,
      renderCell: (params) => (
        <Box
          sx={{
            overflow: "hidden",
            textOverflow: "ellipsis",
            whiteSpace: "nowrap",
            width: "100%",
          }}
          title={params.value}
        >
          {params.value}
        </Box>
      ),
    },
    {
      field: "subtopicName",
      headerName: "Subtopic",
      flex: 1,
      minWidth: 160,
      renderCell: (params) => (
        <Chip
          label={params.value}
          size="small"
          sx={{ bgcolor: "rgba(198, 48, 49, 0.08)", color: "var(--brand-color)", fontWeight: 500 }}
        />
      ),
    },
    {
      field: "topicName",
      headerName: "Topic",
      flex: 1,
      minWidth: 160,
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
          placeholder="Search questions…"
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
        <FormControl size="small" sx={{ minWidth: 180 }}>
          <InputLabel>Filter by topic</InputLabel>
          <Select
            value={topicFilter}
            label="Filter by topic"
            onChange={(e) => handleTopicFilterChange(e.target.value as number | "")}
          >
            <MenuItem value="">All topics</MenuItem>
            {topics.map((t) => (
              <MenuItem key={t.id} value={t.id}>
                {t.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <FormControl size="small" sx={{ minWidth: 200 }}>
          <InputLabel>Filter by subtopic</InputLabel>
          <Select
            value={subtopicFilter}
            label="Filter by subtopic"
            onChange={(e) => onSubtopicFilterChange(e.target.value as number | "")}
          >
            <MenuItem value="">All subtopics</MenuItem>
            {visibleSubtopics.map((s) => (
              <MenuItem key={s.id} value={s.id}>
                {s.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <Box sx={{ flex: 1 }} />
        <Button
          variant="contained"
          startIcon={<Plus size={16} />}
          onClick={onAdd}
          disabled={subtopics.length === 0}
          sx={{
            bgcolor: "var(--brand-color)",
            "&:hover": { bgcolor: "var(--brand-color)", filter: "brightness(0.92)" },
            textTransform: "none",
          }}
        >
          Add question
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
          onRowClick={(params) => onRowClick(params.row.id)}
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
        label="Question"
        onCancel={() => setDeleteId(null)}
        onConfirm={() => {
          if (deleteId !== null) onDelete(deleteId);
          setDeleteId(null);
        }}
      />
    </Stack>
  );
}
