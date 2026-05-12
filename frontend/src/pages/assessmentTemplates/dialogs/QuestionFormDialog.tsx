import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Stack,
  TextField,
  Typography,
  Divider,
} from "@mui/material";
import { useEffect, useState } from "react";
import { questionApi } from "@/shared/api/questionApi.ts";
import type { Translate } from "@/entities/question/QuestionDetails.ts";

interface Props {
  open: boolean;
  questionId: number | null;
  defaultSubtopicId?: number;
  onClose: () => void;
  onCreate: (
    subtopicId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => Promise<void>;
  onUpdate: (
    questionId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => Promise<void>;
}

export default function QuestionFormDialog({
  open,
  questionId,
  defaultSubtopicId,
  onClose,
  onCreate,
  onUpdate,
}: Props) {
  const isEdit = questionId !== null;
  const [contentRu, setContentRu] = useState("");
  const [contentEn, setContentEn] = useState("");
  const [answerRu, setAnswerRu] = useState("");
  const [answerEn, setAnswerEn] = useState("");

  useEffect(() => {
    if (!open) return;

    if (questionId === null) {
      const timer = window.setTimeout(() => {
        setContentRu("");
        setContentEn("");
        setAnswerRu("");
        setAnswerEn("");
      });
      return () => window.clearTimeout(timer);
    }

    let canceled = false;
    questionApi.getById(questionId).then((d) => {
      if (canceled) return;
      setContentRu(d.translates.find((t) => t.language === "ru")?.content ?? "");
      setContentEn(d.translates.find((t) => t.language === "en")?.content ?? "");
      setAnswerRu(d.answer?.translates.find((t) => t.language === "ru")?.content ?? "");
      setAnswerEn(d.answer?.translates.find((t) => t.language === "en")?.content ?? "");
    });
    return () => {
      canceled = true;
    };
  }, [open, questionId]);

  const hasContent = contentRu.trim() !== "" || contentEn.trim() !== "";
  const hasAnswer = answerRu.trim() !== "" || answerEn.trim() !== "";
  const isValid = hasContent && hasAnswer;

  const handleSave = async () => {
    if (!isValid) return;
    const translates: Translate[] = [
      ...(contentRu.trim() ? [{ language: "ru", content: contentRu }] : []),
      ...(contentEn.trim() ? [{ language: "en", content: contentEn }] : []),
    ];
    const answerTranslates: Translate[] = [
      ...(answerRu.trim() ? [{ language: "ru", content: answerRu }] : []),
      ...(answerEn.trim() ? [{ language: "en", content: answerEn }] : []),
    ];
    if (isEdit) {
      await onUpdate(questionId, translates, answerTranslates);
    } else {
      await onCreate(defaultSubtopicId as number, translates, answerTranslates);
    }
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{isEdit ? "Edit Question" : "Create Question"}</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          <Typography variant="overline" sx={{ color: "var(--brand-color)", fontWeight: 600 }}>
            Question
          </Typography>
          <TextField
            label="Content (RU)"
            value={contentRu}
            onChange={(e) => setContentRu(e.target.value)}
            fullWidth
            multiline
            minRows={2}
          />
          <TextField
            label="Content (EN)"
            value={contentEn}
            onChange={(e) => setContentEn(e.target.value)}
            fullWidth
            multiline
            minRows={2}
          />

          <Divider />

          <Typography variant="overline" sx={{ color: "var(--brand-color)", fontWeight: 600 }}>
            Answer
          </Typography>
          <TextField
            label="Answer (RU)"
            value={answerRu}
            onChange={(e) => setAnswerRu(e.target.value)}
            fullWidth
            multiline
            minRows={2}
          />
          <TextField
            label="Answer (EN)"
            value={answerEn}
            onChange={(e) => setAnswerEn(e.target.value)}
            fullWidth
            multiline
            minRows={2}
          />
        </Stack>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button variant="contained" onClick={handleSave} disabled={!isValid}>
          {isEdit ? "Save" : "Create"}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
