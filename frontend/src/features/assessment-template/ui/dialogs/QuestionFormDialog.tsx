import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
  Typography,
  Divider,
} from "@mui/material";
import { useEffect, useState } from "react";
import { questionApi } from "@/features/assessment-template/api/questionApi.ts";
import type { Translate } from "@/features/assessment-template/model/QuestionDetails.ts";

interface SubtopicOption {
  id: number;
  name: string;
}

interface Props {
  open: boolean;
  questionId: number | null;
  defaultSubtopicId?: number;
  subtopics?: SubtopicOption[];
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
  subtopics,
  onClose,
  onCreate,
  onUpdate,
}: Props) {
  const isEdit = questionId !== null;
  const showSubtopicSelector = !isEdit && Array.isArray(subtopics);

  const [subtopicId, setSubtopicId] = useState<number | "">("");
  const [contentRu, setContentRu] = useState("");
  const [contentEn, setContentEn] = useState("");
  const [answerRu, setAnswerRu] = useState("");
  const [answerEn, setAnswerEn] = useState("");

  useEffect(() => {
    if (!open) return;

    if (questionId === null) {
      const timer = window.setTimeout(() => {
        setSubtopicId(defaultSubtopicId ?? "");
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
  }, [open, questionId, defaultSubtopicId]);

  const hasContent = contentRu.trim() !== "" || contentEn.trim() !== "";
  const hasAnswer = answerRu.trim() !== "" || answerEn.trim() !== "";
  const hasSubtopic = isEdit || subtopicId !== "";
  const isValid = hasContent && hasAnswer && hasSubtopic;

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
      await onCreate(subtopicId as number, translates, answerTranslates);
    }
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{isEdit ? "Edit Question" : "Create Question"}</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          {showSubtopicSelector && (
            <FormControl fullWidth required>
              <InputLabel>Subtopic *</InputLabel>
              <Select
                value={subtopicId}
                label="Subtopic *"
                onChange={(e) => setSubtopicId(e.target.value as number)}
              >
                {subtopics!.map((s) => (
                  <MenuItem key={s.id} value={s.id}>
                    {s.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          )}
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
