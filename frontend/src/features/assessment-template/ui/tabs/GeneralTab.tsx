import { Box, Button, CircularProgress, Chip, Divider, Stack, Typography } from "@mui/material";
import { RefreshCw } from "lucide-react";
import type { SubtopicRow } from "./SubtopicsTab.tsx";
import type { QuestionRow } from "./QuestionsTab.tsx";

interface Props {
  topics: { id: number; name: string }[];
  subtopics: SubtopicRow[];
  questions: QuestionRow[];
  questionsLoaded: boolean;
  questionsLoading: boolean;
  onLoadQuestions: () => void;
  onTopicClick: (topicId: number) => void;
  onSubtopicClick: (subtopicId: number) => void;
  onQuestionClick: (questionId: number, subtopicId: number) => void;
}

function ColumnHeader({ label, count }: { label: string; count?: number }) {
  return (
    <Stack direction="row" spacing={1} alignItems="center" sx={{ px: 2, py: 1.5, flexShrink: 0 }}>
      <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
        {label}
      </Typography>
      {count !== undefined && (
        <Chip
          label={count}
          size="small"
          sx={{
            bgcolor: "rgba(198, 48, 49, 0.08)",
            color: "var(--brand-color)",
            fontWeight: 600,
            height: 20,
            fontSize: 11,
          }}
        />
      )}
    </Stack>
  );
}

function ListItem({
  primary,
  secondary,
  onClick,
}: {
  primary: string;
  secondary?: string;
  onClick: () => void;
}) {
  return (
    <Box
      onClick={onClick}
      sx={{
        px: 2,
        py: 0.75,
        cursor: "pointer",
        borderRadius: 1,
        mx: 0.5,
        "&:hover": { bgcolor: "rgba(198, 48, 49, 0.05)" },
      }}
    >
      <Typography
        variant="body2"
        sx={{ fontWeight: 500, overflow: "hidden", textOverflow: "ellipsis", whiteSpace: "nowrap" }}
      >
        {primary}
      </Typography>
      {secondary && (
        <Typography
          variant="caption"
          color="text.secondary"
          sx={{
            overflow: "hidden",
            textOverflow: "ellipsis",
            whiteSpace: "nowrap",
            display: "block",
          }}
        >
          {secondary}
        </Typography>
      )}
    </Box>
  );
}

function EmptyHint({ text }: { text: string }) {
  return (
    <Typography variant="body2" color="text.secondary" sx={{ px: 2, py: 2 }}>
      {text}
    </Typography>
  );
}

export default function GeneralTab({
  topics,
  subtopics,
  questions,
  questionsLoaded,
  questionsLoading,
  onLoadQuestions,
  onTopicClick,
  onSubtopicClick,
  onQuestionClick,
}: Props) {
  return (
    <Box
      sx={{
        flex: 1,
        minHeight: 0,
        display: "flex",
        border: "1px solid var(--border-color, rgba(0,0,0,0.12))",
        borderRadius: 2,
        overflow: "hidden",
      }}
    >
      {/* Topics */}
      <Stack sx={{ flex: 1, minWidth: 0, minHeight: 0 }}>
        <ColumnHeader label="Topics" count={topics.length} />
        <Divider />
        <Box sx={{ flex: 1, overflowY: "auto", py: 0.5 }}>
          {topics.length === 0 ? (
            <EmptyHint text="No topics" />
          ) : (
            topics.map((t) => (
              <ListItem key={t.id} primary={t.name} onClick={() => onTopicClick(t.id)} />
            ))
          )}
        </Box>
      </Stack>

      <Divider orientation="vertical" flexItem />

      {/* Subtopics */}
      <Stack sx={{ flex: 1, minWidth: 0, minHeight: 0 }}>
        <ColumnHeader label="Subtopics" count={subtopics.length} />
        <Divider />
        <Box sx={{ flex: 1, overflowY: "auto", py: 0.5 }}>
          {subtopics.length === 0 ? (
            <EmptyHint text="No subtopics" />
          ) : (
            subtopics.map((s) => (
              <ListItem
                key={s.id}
                primary={s.name}
                secondary={s.topicName}
                onClick={() => onSubtopicClick(s.id)}
              />
            ))
          )}
        </Box>
      </Stack>

      <Divider orientation="vertical" flexItem />

      {/* Questions */}
      <Stack sx={{ flex: 1, minWidth: 0, minHeight: 0 }}>
        <ColumnHeader label="Questions" count={questionsLoaded ? questions.length : undefined} />
        <Divider />
        <Box sx={{ flex: 1, overflowY: "auto", py: 0.5 }}>
          {questionsLoading ? (
            <Stack alignItems="center" justifyContent="center" sx={{ py: 4 }}>
              <CircularProgress size={24} sx={{ color: "var(--brand-color)" }} />
            </Stack>
          ) : !questionsLoaded ? (
            <Stack alignItems="center" sx={{ py: 4, px: 2 }} spacing={1.5}>
              <Typography variant="body2" color="text.secondary" textAlign="center">
                Questions haven't been loaded yet
              </Typography>
              <Button
                size="small"
                variant="outlined"
                startIcon={<RefreshCw size={14} />}
                onClick={onLoadQuestions}
                sx={{
                  textTransform: "none",
                  borderColor: "var(--brand-color)",
                  color: "var(--brand-color)",
                  "&:hover": {
                    borderColor: "var(--brand-color)",
                    bgcolor: "rgba(198, 48, 49, 0.04)",
                  },
                }}
              >
                Load questions
              </Button>
            </Stack>
          ) : questions.length === 0 ? (
            <EmptyHint text="No questions" />
          ) : (
            questions.map((q) => (
              <ListItem
                key={q.id}
                primary={q.content}
                secondary={q.subtopicName}
                onClick={() => onQuestionClick(q.id, q.subtopicId)}
              />
            ))
          )}
        </Box>
      </Stack>
    </Box>
  );
}
