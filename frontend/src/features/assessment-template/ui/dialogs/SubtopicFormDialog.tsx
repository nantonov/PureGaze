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
} from "@mui/material";
import { useEffect, useState } from "react";
import { subtopicApi } from "@/features/assessment-template/api/subtopicApi.ts";
import type { SubtopicTranslate } from "@/features/assessment-template/model/SubtopicDetails.ts";

interface TopicOption {
  id: number;
  name: string;
}

interface Props {
  open: boolean;
  subtopicId: number | null;
  topics?: TopicOption[];
  defaultTopicId?: number;
  onClose: () => void;
  onCreate: (topicId: number, translates: SubtopicTranslate[]) => Promise<void>;
  onUpdate: (subtopicId: number, translates: SubtopicTranslate[]) => Promise<void>;
}

export default function SubtopicFormDialog({
  open,
  subtopicId,
  topics,
  defaultTopicId,
  onClose,
  onCreate,
  onUpdate,
}: Props) {
  const isEdit = subtopicId !== null;
  const showTopicSelector = !isEdit && Array.isArray(topics);

  const [nameRu, setNameRu] = useState("");
  const [nameEn, setNameEn] = useState("");
  const [topicId, setTopicId] = useState<number | "">("");

  useEffect(() => {
    if (!open) return;

    if (subtopicId === null) {
      const timer = window.setTimeout(() => {
        setNameRu("");
        setNameEn("");
        setTopicId(defaultTopicId ?? "");
      });
      return () => window.clearTimeout(timer);
    }

    let canceled = false;
    subtopicApi.getById(subtopicId).then((details) => {
      if (canceled) return;
      setNameRu(details.translates.find((t) => t.language === "ru")?.name ?? "");
      setNameEn(details.translates.find((t) => t.language === "en")?.name ?? "");
    });

    return () => {
      canceled = true;
    };
  }, [open, subtopicId, defaultTopicId]);

  const hasName = nameRu.trim() !== "" || nameEn.trim() !== "";
  const hasTopic = !showTopicSelector || topicId !== "";
  const isValid = hasName && hasTopic;

  const handleSave = async () => {
    if (!isValid) return;
    const translates: SubtopicTranslate[] = [
      ...(nameRu.trim() ? [{ language: "ru", name: nameRu }] : []),
      ...(nameEn.trim() ? [{ language: "en", name: nameEn }] : []),
    ];
    if (isEdit) {
      await onUpdate(subtopicId, translates);
    } else {
      const effectiveTopicId = topicId === "" ? (defaultTopicId as number) : (topicId as number);
      await onCreate(effectiveTopicId, translates);
    }
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{isEdit ? "Edit Subtopic" : "Create Subtopic"}</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          {showTopicSelector && (
            <FormControl fullWidth required>
              <InputLabel>Topic *</InputLabel>
              <Select
                value={topicId}
                label="Topic *"
                onChange={(e) => setTopicId(e.target.value as number)}
              >
                {topics!.map((t) => (
                  <MenuItem key={t.id} value={t.id}>
                    {t.name}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          )}
          <TextField
            label="Name (RU)"
            value={nameRu}
            onChange={(e) => setNameRu(e.target.value)}
            fullWidth
          />
          <TextField
            label="Name (EN)"
            value={nameEn}
            onChange={(e) => setNameEn(e.target.value)}
            fullWidth
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
