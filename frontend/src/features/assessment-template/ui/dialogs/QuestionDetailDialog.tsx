import {
  Box,
  Chip,
  CircularProgress,
  Dialog,
  DialogContent,
  DialogTitle,
  Divider,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import { Pencil, X } from "lucide-react";
import { useEffect, useState } from "react";
import type { QuestionDetails } from "@/features/assessment-template/model/QuestionDetails.ts";
import { questionApi } from "@/features/assessment-template/api/questionApi.ts";

const LANG_LABEL: Record<string, string> = { en: "EN", ru: "RU" };

interface Props {
  questionId: number | null;
  onClose: () => void;
  onEdit: (questionId: number) => void;
}

export default function QuestionDetailDialog({ questionId, onClose, onEdit }: Props) {
  const [details, setDetails] = useState<QuestionDetails | null>(null);
  const [fetchedId, setFetchedId] = useState<number | null>(null);

  useEffect(() => {
    if (questionId === null) return;
    let canceled = false;
    questionApi.getById(questionId).then((d) => {
      if (canceled) return;
      setDetails(d);
      setFetchedId(questionId);
    });
    return () => {
      canceled = true;
    };
  }, [questionId]);

  const loading = questionId !== null && fetchedId !== questionId;

  return (
    <Dialog open={questionId !== null} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle sx={{ pr: 6 }}>
        <Stack direction="row" alignItems="center" justifyContent="space-between">
          <Typography variant="h6" component="span">
            Question details
          </Typography>
          <Stack direction="row" spacing={0.5}>
            {details && (
              <IconButton size="small" onClick={() => onEdit(details.id)} title="Edit question">
                <Pencil size={16} />
              </IconButton>
            )}
            <IconButton size="small" onClick={onClose}>
              <X size={18} />
            </IconButton>
          </Stack>
        </Stack>
      </DialogTitle>

      <DialogContent>
        {loading ? (
          <Stack alignItems="center" justifyContent="center" sx={{ py: 4 }}>
            <CircularProgress size={32} sx={{ color: "var(--brand-color)" }} />
          </Stack>
        ) : details ? (
          <Stack spacing={3}>
            {/* Question translates */}
            <Box>
              <Typography
                variant="overline"
                sx={{ color: "var(--brand-color)", letterSpacing: 1.2, fontWeight: 600 }}
              >
                Question
              </Typography>
              <Stack spacing={1.5} sx={{ mt: 1 }}>
                {details.translates.length === 0 ? (
                  <Typography variant="body2" color="text.secondary">
                    No content
                  </Typography>
                ) : (
                  details.translates.map((t) => (
                    <Stack key={t.language} direction="row" spacing={1.5} alignItems="flex-start">
                      <Chip
                        label={LANG_LABEL[t.language] ?? t.language.toUpperCase()}
                        size="small"
                        sx={{
                          mt: 0.25,
                          minWidth: 36,
                          bgcolor: "rgba(198, 48, 49, 0.08)",
                          color: "var(--brand-color)",
                          fontWeight: 600,
                          flexShrink: 0,
                        }}
                      />
                      <Typography variant="body2" sx={{ whiteSpace: "pre-wrap" }}>
                        {t.content}
                      </Typography>
                    </Stack>
                  ))
                )}
              </Stack>
            </Box>

            <Divider />

            {/* Answer translates */}
            <Box>
              <Typography
                variant="overline"
                sx={{ color: "var(--brand-color)", letterSpacing: 1.2, fontWeight: 600 }}
              >
                Answer
              </Typography>
              <Stack spacing={1.5} sx={{ mt: 1 }}>
                {!details.answer || details.answer.translates.length === 0 ? (
                  <Typography variant="body2" color="text.secondary">
                    No answer
                  </Typography>
                ) : (
                  details.answer.translates.map((t) => (
                    <Stack key={t.language} direction="row" spacing={1.5} alignItems="flex-start">
                      <Chip
                        label={LANG_LABEL[t.language] ?? t.language.toUpperCase()}
                        size="small"
                        sx={{
                          mt: 0.25,
                          minWidth: 36,
                          bgcolor: "rgba(198, 48, 49, 0.08)",
                          color: "var(--brand-color)",
                          fontWeight: 600,
                          flexShrink: 0,
                        }}
                      />
                      <Typography variant="body2" sx={{ whiteSpace: "pre-wrap" }}>
                        {t.content}
                      </Typography>
                    </Stack>
                  ))
                )}
              </Stack>
            </Box>
          </Stack>
        ) : null}
      </DialogContent>
    </Dialog>
  );
}
