import { Chip, Stack, Typography } from "@mui/material";
import type { Template } from "@/entities/template/Template.ts";

interface Props {
  template: Template;
  topicsCount: number;
  subtopicsCount: number;
  questionsCount: number | null;
}

export default function TemplateDetailHeader({
  template,
  topicsCount,
  subtopicsCount,
  questionsCount,
}: Props) {
  return (
    <Stack spacing={0.5}>
      <Stack direction="row" spacing={1} alignItems="center">
        <Typography variant="h6" sx={{ fontWeight: 600 }}>
          {template.codeName || `Template #${template.id}`}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          {template.codeDisplay}
        </Typography>
      </Stack>
      <Stack direction="row" spacing={1} sx={{ mt: 0.5 }}>
        <Chip
          label={`${topicsCount} ${topicsCount === 1 ? "topic" : "topics"}`}
          size="small"
          sx={{ bgcolor: "rgba(198, 48, 49, 0.08)", color: "var(--brand-color)" }}
        />
        <Chip
          label={`${subtopicsCount} ${subtopicsCount === 1 ? "subtopic" : "subtopics"}`}
          size="small"
          sx={{ bgcolor: "rgba(198, 48, 49, 0.08)", color: "var(--brand-color)" }}
        />
        {questionsCount !== null && (
          <Chip
            label={`${questionsCount} ${questionsCount === 1 ? "question" : "questions"}`}
            size="small"
            sx={{ bgcolor: "rgba(198, 48, 49, 0.08)", color: "var(--brand-color)" }}
          />
        )}
      </Stack>
    </Stack>
  );
}
