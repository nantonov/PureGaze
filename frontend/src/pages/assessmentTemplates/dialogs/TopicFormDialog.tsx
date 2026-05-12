import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Stack,
  TextField,
} from "@mui/material";
import { useEffect, useState } from "react";
import type { Topic, TopicTranslate } from "@/entities/template/Topic.ts";
import { topicApi } from "@/shared/api/topicApi.ts";

interface Props {
  open: boolean;
  topic: Topic | null;
  onClose: () => void;
  onCreate: (translates: TopicTranslate[]) => Promise<void>;
  onUpdate: (topicId: number, translates: TopicTranslate[]) => Promise<void>;
}

interface FormContentProps {
  topic: Topic | null;
  onClose: () => void;
  onCreate: (translates: TopicTranslate[]) => Promise<void>;
  onUpdate: (topicId: number, translates: TopicTranslate[]) => Promise<void>;
}

function TopicFormDialogContent({ topic, onClose, onCreate, onUpdate }: FormContentProps) {
  const isEdit = topic !== null;
  const [nameRu, setNameRu] = useState("");
  const [nameEn, setNameEn] = useState("");

  useEffect(() => {
    if (!isEdit) {
      const timer = window.setTimeout(() => {
        setNameRu("");
        setNameEn("");
      });
      return () => window.clearTimeout(timer);
    }
    let canceled = false;
    topicApi.getById(topic.id).then((d) => {
      if (canceled) return;
      setNameRu(d.translates.find((t) => t.language === "ru")?.name ?? "");
      setNameEn(d.translates.find((t) => t.language === "en")?.name ?? "");
    });
    return () => {
      canceled = true;
    };
  }, [isEdit, topic]);

  const handleSave = async () => {
    if (!nameRu.trim() && !nameEn.trim()) return;
    const translates: TopicTranslate[] = [
      ...(nameRu.trim() ? [{ language: "ru", name: nameRu }] : []),
      ...(nameEn.trim() ? [{ language: "en", name: nameEn }] : []),
    ];
    if (isEdit) {
      await onUpdate(topic.id, translates);
    } else {
      await onCreate(translates);
    }
    onClose();
  };

  const isValid = nameRu.trim() !== "" || nameEn.trim() !== "";

  return (
    <>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
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
    </>
  );
}

export default function TopicFormDialog({ open, topic, onClose, onCreate, onUpdate }: Props) {
  const isEdit = topic !== null;

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{isEdit ? "Edit Topic" : "Create Topic"}</DialogTitle>
      {open && (
        <TopicFormDialogContent
          key={topic ? `topic-${topic.id}` : "new-topic"}
          topic={topic}
          onClose={onClose}
          onCreate={onCreate}
          onUpdate={onUpdate}
        />
      )}
    </Dialog>
  );
}
