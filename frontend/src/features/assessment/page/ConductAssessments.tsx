import { useEffect, useState } from "react";
import { Box, Button, CircularProgress, Stack, Typography } from "@mui/material";
import PageContentBox from "@/widgets/tableBox/PageContentBox";
import { assessmentApi } from "@/features/assessment/api/assessmentApi";
import type { AssignedAssessmentStage } from "@/features/assessment/model/AssignedAssessmentStage";
import AssessmentQuestionnaire from "@/features/assessment/ui/AssessmentQuestionnaire";

export default function ConductAssessments() {
  const [items, setItems] = useState<AssignedAssessmentStage[]>([]);
  const [selected, setSelected] = useState<AssignedAssessmentStage | null>(null);
  const [loading, setLoading] = useState(true);

  const load = async () => {
    setLoading(true);
    try {
      setItems((await assessmentApi.getAssignedToMe()).items);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    // Initial remote data loading is intentionally tied to mounting this page.
    // eslint-disable-next-line react-hooks/set-state-in-effect
    void load();
  }, []);

  if (selected) {
    return (
      <AssessmentQuestionnaire
        stage={selected}
        onBack={() => {
          setSelected(null);
          void load();
        }}
      />
    );
  }

  return (
    <PageContentBox>
      <Typography variant="h4">Assessments</Typography>
      {loading ? (
        <CircularProgress />
      ) : items.length === 0 ? (
        <Typography color="text.secondary">No topics are currently assigned to you.</Typography>
      ) : (
        <Stack gap={1.5}>
          {items.map((item) => (
            <Box
              key={item.id}
              sx={{
                border: "1px solid",
                borderColor: "divider",
                borderRadius: 2,
                p: 2,
                display: "flex",
                alignItems: "center",
                gap: 2,
              }}
            >
              <Box sx={{ flex: 1 }}>
                <Typography fontWeight={700}>{item.topicName}</Typography>
                <Typography variant="body2">
                  {item.employeeFullName} · {item.employeeEmail}
                </Typography>
              </Box>
              <Button variant="contained" onClick={() => setSelected(item)}>
                Conduct
              </Button>
            </Box>
          ))}
        </Stack>
      )}
    </PageContentBox>
  );
}
