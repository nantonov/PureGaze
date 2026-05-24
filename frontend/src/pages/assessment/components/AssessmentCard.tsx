import { useState } from "react";
import { Box, Typography, Button, CircularProgress, Stack, Divider, Chip } from "@mui/material";
import { ArrowRight, UserCheck } from "lucide-react";
import type { AssessmentListItem } from "@/entities/assessment/Assessment";
import { assessmentApi } from "@/shared/api/assessmentApi";
import { ApiError } from "@/shared/api/baseApi";
import ErrorSnackbar from "@/shared/ui/ErrorSnackbar";

interface Props {
  assessment: AssessmentListItem;
  onAssigned: () => void;
}

export default function AssessmentCard({ assessment, onAssigned }: Props) {
  const [loadingStageId, setLoadingStageId] = useState<number | null>(null);
  const [error, setError] = useState<string | null>(null);
  const userAlreadyAssigned = assessment.stages.some((s) => s.isAssignedToCurrentUser);

  const handleAssign = async (stageId: number) => {
    setLoadingStageId(stageId);
    try {
      await assessmentApi.assignStage(stageId);
      onAssigned();
    } catch (err) {
      const detail =
        err instanceof ApiError && err.data && typeof err.data === "object"
          ? (err.data as { detail?: string }).detail
          : undefined;
      setError(detail ?? "Failed to assign. Please try again.");
    } finally {
      setLoadingStageId(null);
    }
  };

  const handleUnassign = async (stageId: number) => {
    try {
      await assessmentApi.unassignStage(stageId);
      onAssigned();
    } catch (err) {
      const detail =
        err instanceof ApiError && err.data && typeof err.data === "object"
          ? (err.data as { detail?: string }).detail
          : undefined;
      setError(detail ?? "Failed to unassign. Please try again.");
    }
  };

  const [fromGrade, toGrade] = assessment.gradeRange.split(" -> ");

  return (
    <>
      <Box
        sx={{
          border: "1px solid",
          borderColor: "divider",
          borderRadius: 2,
          overflow: "hidden",
          display: "flex",
          bgcolor: "var(--board-task-background-color)",
          boxShadow: "0 1px 3px rgba(0,0,0,0.06)",
        }}
      >
        {/* Employee column */}
        <Box
          sx={{
            p: 2.5,
            minWidth: 200,
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            position: "relative",
          }}
        >
          <Typography
            variant="caption"
            sx={{
              position: "absolute",
              top: 6,
              left: 10,
              color: "text.disabled",
              fontSize: "0.65rem",
            }}
          >
            #{assessment.id}
          </Typography>
          <Typography variant="subtitle1" fontWeight={700} lineHeight={1.3}>
            {assessment.employeeFullName}
          </Typography>
          <Typography variant="caption" color="text.secondary" sx={{ mt: 0.25 }}>
            {assessment.employeeEmail}
          </Typography>
        </Box>

        <Divider orientation="vertical" flexItem />

        {/* Grade column */}
        <Box
          sx={{
            px: 2.5,
            minWidth: 160,
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Stack direction="row" alignItems="center" spacing={0.75}>
            <Typography variant="body2" fontWeight={600} color="text.primary">
              {fromGrade}
            </Typography>
            <ArrowRight size={14} color="var(--text-color)" />
            <Typography variant="body2" fontWeight={600} color="text.primary">
              {toGrade}
            </Typography>
          </Stack>
        </Box>

        <Divider orientation="vertical" flexItem />

        {/* Topics column */}
        <Box sx={{ flex: 1, p: 1 }}>
          {assessment.stages.map((stage, index) => (
            <Stack
              key={stage.id}
              direction="row"
              alignItems="center"
              justifyContent="space-between"
              sx={{
                px: 1.5,
                py: 0.5,
                borderRadius: 1.5,
                bgcolor: index % 2 === 0 ? "var(--row-stripe-bg)" : "transparent",
              }}
            >
              <Box sx={{ width: 150, flexShrink: 0 }}>
                {stage.assessorFullName ? (
                  <Chip
                    icon={<UserCheck size={15} />}
                    label={stage.assessorFullName}
                    size="small"
                    variant="outlined"
                    onDelete={
                      stage.isAssignedToCurrentUser ? () => handleUnassign(stage.id) : undefined
                    }
                    sx={{
                      fontSize: "0.76rem",
                      padding: "2px 6px",
                      height: 28,
                      borderColor: "success.light",
                      color: "success.dark",
                    }}
                  />
                ) : (
                  <Button
                    size="small"
                    variant="contained"
                    disableElevation
                    disabled={
                      loadingStageId !== null || assessment.isOwnAssessment || userAlreadyAssigned
                    }
                    onClick={() => handleAssign(stage.id)}
                    sx={{
                      bgcolor: "var(--brand-color)",
                      fontSize: "0.72rem",
                      height: 28,
                      fontWeight: 600,
                      letterSpacing: 0.3,
                      borderRadius: 3.5,
                      px: 1.5,
                      width: "100%",
                      "&:hover": { bgcolor: "var(--brand-color-hover)" },
                    }}
                  >
                    {loadingStageId === stage.id ? (
                      <CircularProgress size={13} sx={{ color: "white" }} />
                    ) : (
                      "Assign"
                    )}
                  </Button>
                )}
              </Box>

              <Typography
                variant="body1"
                fontWeight={700}
                noWrap
                minWidth={200}
                sx={{ letterSpacing: 0.1, flex: 1, textAlign: "left" }}
                marginLeft={2}
              >
                {stage.topicName}
              </Typography>
            </Stack>
          ))}
        </Box>
      </Box>

      <ErrorSnackbar message={error} onClose={() => setError(null)} />
    </>
  );
}
