import { useState } from "react";
import {
  Box,
  Button,
  FormControl,
  FormControlLabel,
  MenuItem,
  Radio,
  RadioGroup,
  Select,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { ArrowLeft } from "lucide-react";
import { assessmentApi } from "@/features/assessment/api/assessmentApi";
import type {
  AssessmentMark,
  AssignedAssessmentStage,
} from "@/features/assessment/model/AssignedAssessmentStage";
import ErrorSnackbar from "@/shared/ui/ErrorSnackbar";

interface Props {
  stage: AssignedAssessmentStage;
  onBack: () => void;
}

const marks: { value: AssessmentMark; label: string }[] = [
  { value: "NeedsImprovement", label: "Needs improvement" },
  { value: "Competent", label: "Competent" },
  { value: "Excellent", label: "Excellent" },
];

export default function AssessmentQuestionnaire({ stage, onBack }: Props) {
  const [scores, setScores] = useState<Record<number, AssessmentMark | "">>(
    Object.fromEntries(stage.subtopics.map((x) => [x.id, x.score ?? ""])),
  );
  const [comments, setComments] = useState<Record<number, string>>(
    Object.fromEntries(stage.subtopics.map((x) => [x.id, x.comment ?? ""])),
  );
  const [recommended, setRecommended] = useState("yes");
  const [summary, setSummary] = useState("");
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const submit = async () => {
    if (stage.subtopics.some((x) => !scores[x.id])) {
      setError("Please rate every subtopic before completing the assessment.");
      return;
    }
    setSaving(true);
    try {
      if (stage.status === "Pending") await assessmentApi.startStage(stage.id);
      await Promise.all(
        stage.subtopics.map((x) =>
          assessmentApi.scoreSubtopic(
            stage.id,
            x.id,
            scores[x.id] as AssessmentMark,
            comments[x.id] ?? "",
          ),
        ),
      );
      await assessmentApi.completeStage(stage.id, recommended === "yes", summary);
      onBack();
    } catch {
      setError("The assessment could not be saved. Please try again.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <PageShell>
      <Stack direction="row" alignItems="center" gap={1}>
        <Button startIcon={<ArrowLeft size={17} />} onClick={onBack}>
          Back
        </Button>
        <Box>
          <Typography variant="h5" fontWeight={700}>
            {stage.topicName}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Assessment of {stage.employeeFullName}
          </Typography>
        </Box>
      </Stack>

      <Box sx={{ overflow: "auto", border: "1px solid", borderColor: "divider", borderRadius: 2 }}>
        {stage.subtopics.map((subtopic, index) => (
          <Box
            key={subtopic.id}
            sx={{
              display: "grid",
              gridTemplateColumns: {
                xs: "1fr",
                md: "180px minmax(260px, 1fr) minmax(260px, 1fr) 190px 260px",
              },
              borderTop: index ? "1px solid" : 0,
              borderColor: "divider",
            }}
          >
            <Box
              sx={{
                p: 2,
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                bgcolor: "action.hover",
              }}
            >
              <Typography fontWeight={700} textAlign="center">
                {subtopic.name}
              </Typography>
            </Box>
            <Box sx={{ p: 2 }}>
              {subtopic.questions.map((q) => (
                <Typography key={q.id} variant="body2" sx={{ mb: 1.25 }}>
                  {q.content}
                </Typography>
              ))}
            </Box>
            <Box sx={{ p: 2, bgcolor: "rgba(124, 179, 66, 0.08)" }}>
              {subtopic.questions.map((q) => (
                <Typography key={q.id} variant="body2" sx={{ mb: 1.25 }}>
                  {q.hint || "—"}
                </Typography>
              ))}
            </Box>
            <Box sx={{ p: 2 }}>
              <FormControl fullWidth size="small">
                <Select
                  value={scores[subtopic.id] ?? ""}
                  displayEmpty
                  onChange={(e) =>
                    setScores((old) => ({
                      ...old,
                      [subtopic.id]: e.target.value as AssessmentMark,
                    }))
                  }
                >
                  <MenuItem value="" disabled>
                    Rating
                  </MenuItem>
                  {marks.map((mark) => (
                    <MenuItem key={mark.value} value={mark.value}>
                      {mark.label}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Box>
            <Box sx={{ p: 2 }}>
              <TextField
                fullWidth
                multiline
                minRows={3}
                placeholder="Note"
                value={comments[subtopic.id] ?? ""}
                onChange={(e) => setComments((old) => ({ ...old, [subtopic.id]: e.target.value }))}
              />
            </Box>
          </Box>
        ))}
      </Box>

      <Box sx={{ border: "1px solid", borderColor: "divider", borderRadius: 2, p: 2 }}>
        <Typography fontWeight={700}>Move to next level recommendation</Typography>
        <RadioGroup row value={recommended} onChange={(e) => setRecommended(e.target.value)}>
          <FormControlLabel value="yes" control={<Radio />} label="Yes" />
          <FormControlLabel value="no" control={<Radio />} label="No" />
        </RadioGroup>
        <TextField
          fullWidth
          multiline
          minRows={3}
          label="General feedback"
          value={summary}
          onChange={(e) => setSummary(e.target.value)}
          sx={{ mt: 1 }}
        />
        <Stack direction="row" justifyContent="flex-end" mt={2}>
          <Button variant="contained" disabled={saving} onClick={submit}>
            {saving ? "Saving…" : "Complete assessment"}
          </Button>
        </Stack>
      </Box>
      <ErrorSnackbar message={error} onClose={() => setError(null)} />
    </PageShell>
  );
}

function PageShell({ children }: { children: React.ReactNode }) {
  return (
    <Box
      sx={{
        p: 3,
        display: "flex",
        flexDirection: "column",
        gap: 2,
        height: "100%",
        overflow: "auto",
      }}
    >
      {children}
    </Box>
  );
}
