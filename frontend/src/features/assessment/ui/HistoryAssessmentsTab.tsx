import { useState, useCallback, useEffect, useMemo } from "react";
import { Box, Chip, TextField, InputAdornment } from "@mui/material";
import { Search as SearchIcon } from "lucide-react";
import { useDebounce } from "@/shared/hooks/useDebounce";
import { type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";
import { assessmentApi } from "@/features/assessment/api/assessmentApi";
import type { AssessmentHistoryItem } from "@/features/assessment/model/Assessment";
import { PaginationTable } from "@/shared/ui/table/pagination/PaginationTable";

const STATUS_COLORS: Record<string, "warning" | "success" | "default"> = {
  Finished: "success",
  Closed: "default",
};

export default function HistoryAssessmentsTab() {
  const [rows, setRows] = useState<AssessmentHistoryItem[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(false);
  const [search, setSearch] = useState("");
  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
    page: 0,
    pageSize: 10,
  });

  const debouncedSearch = useDebounce(search, 300);

  const fetchData = useCallback(async () => {
    setLoading(true);
    try {
      const res = await assessmentApi.getHistory({
        page: paginationModel.page + 1,
        pageSize: paginationModel.pageSize,
        search: debouncedSearch || undefined,
      });
      setRows(res.items);
      setTotal(res.total);
    } finally {
      setLoading(false);
    }
  }, [paginationModel, debouncedSearch]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleSearch = (value: string) => {
    setSearch(value);
    setPaginationModel((prev) => ({ ...prev, page: 0 }));
  };

  const columns = useMemo<GridColDef<AssessmentHistoryItem>[]>(
    () => [
      { field: "id", headerName: "ID", width: 80 },
      { field: "employeeFullName", headerName: "Employee", flex: 1 },
      { field: "employeeEmail", headerName: "Email", flex: 1 },
      { field: "gradeRange", headerName: "Grade Range", flex: 1 },
      {
        field: "status",
        headerName: "Status",
        width: 130,
        renderCell: ({ value }) => (
          <Chip label={value} size="small" color={STATUS_COLORS[value] ?? "default"} />
        ),
      },
      {
        field: "createdAt",
        headerName: "Created",
        width: 120,
        renderCell: ({ value }) => new Date(value).toLocaleDateString(),
      },
    ],
    [],
  );

  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 2, flex: 1, minHeight: 0 }}>
      <TextField
        size="small"
        placeholder="Search"
        value={search}
        onChange={(e) => handleSearch(e.target.value)}
        sx={{ maxWidth: 320 }}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon size={16} />
            </InputAdornment>
          ),
        }}
      />

      <Box sx={{ flex: 1, minHeight: 0 }}>
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
    </Box>
  );
}
