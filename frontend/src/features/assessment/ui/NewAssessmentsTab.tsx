import { useState, useEffect, useMemo } from "react";
import { Box, TextField, CircularProgress, Typography, InputAdornment, Stack } from "@mui/material";
import { Search as SearchIcon } from "lucide-react";
import type { AssessmentListItem } from "@/features/assessment/model/Assessment";
import { assessmentApi } from "@/features/assessment/api/assessmentApi";
import { useDebounce } from "@/shared/hooks/useDebounce";
import AssessmentCard from "@/features/assessment/ui/AssessmentCard";

export default function NewAssessmentsTab() {
  const [search, setSearch] = useState("");
  const [allRows, setAllRows] = useState<AssessmentListItem[]>([]);
  const [loading, setLoading] = useState(false);

  const debouncedSearch = useDebounce(search, 300);

  const fetchData = async () => {
    setLoading(true);
    try {
      const res = await assessmentApi.getNew();
      setAllRows(res.items);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const filteredRows = useMemo(() => {
    const q = debouncedSearch.trim().toLowerCase();
    if (!q) return allRows;
    return allRows.filter(
      (a) =>
        a.employeeFullName.toLowerCase().includes(q) ||
        a.employeeEmail.toLowerCase().includes(q) ||
        a.gradeRange.toLowerCase().includes(q) ||
        a.stages.some((s) => s.topicName.toLowerCase().includes(q)),
    );
  }, [allRows, debouncedSearch]);

  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 2, flex: 1, overflow: "hidden" }}>
      <TextField
        size="small"
        placeholder="Search"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        sx={{ maxWidth: 320 }}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon size={16} />
            </InputAdornment>
          ),
        }}
      />

      {loading ? (
        <Box sx={{ display: "flex", justifyContent: "center", pt: 4 }}>
          <CircularProgress />
        </Box>
      ) : filteredRows.length === 0 ? (
        <Typography color="text.secondary" sx={{ pt: 2 }}>
          {search ? "No assessments match your search." : "No new assessments available."}
        </Typography>
      ) : (
        <Box sx={{ overflowX: "auto", flex: 1 }}>
          <Stack gap={1.5} sx={{ overflowY: "auto", minWidth: 760 }}>
            {filteredRows.map((a) => (
              <AssessmentCard key={a.id} assessment={a} onAssigned={fetchData} />
            ))}
          </Stack>
        </Box>
      )}
    </Box>
  );
}
