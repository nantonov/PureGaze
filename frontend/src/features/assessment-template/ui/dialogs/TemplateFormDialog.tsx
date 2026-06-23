import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Stack,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText,
  Alert,
} from "@mui/material";
import { useEffect, useState } from "react";
import { codeApi } from "@/features/code/api/adminCodeApi.ts";
import type { GetCode } from "@/features/code/model/GetCode.ts";
import { ApiError } from "@/shared/api/baseApi.ts";

interface Props {
  open: boolean;
  onClose: () => void;
  onCreate: (codeId: number) => Promise<void>;
}

export default function TemplateFormDialog({ open, onClose, onCreate }: Props) {
  const [codes, setCodes] = useState<GetCode[]>([]);
  const [codeId, setCodeId] = useState<number | "">("");
  const [error, setError] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (!open) return;
    setCodeId("");
    setError(null);
    codeApi.getAll().then(setCodes);
  }, [open]);

  const handleSave = async () => {
    if (codeId === "") return;
    setSaving(true);
    setError(null);
    try {
      await onCreate(codeId as number);
      onClose();
    } catch (e) {
      if (e instanceof ApiError) {
        setError(typeof e.data === "string" ? e.data : "Template with this code already exists.");
      } else {
        setError("Unexpected error occurred.");
      }
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>Create Template</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          {error && <Alert severity="error">{error}</Alert>}
          <FormControl fullWidth error={codeId === ""}>
            <InputLabel>Code *</InputLabel>
            <Select
              value={codeId}
              label="Code *"
              onChange={(e) => setCodeId(e.target.value as number)}
            >
              {codes.map((c) => (
                <MenuItem key={c.id} value={c.id}>
                  {c.display || c.name}
                </MenuItem>
              ))}
            </Select>
            {codeId === "" && <FormHelperText>Code is required</FormHelperText>}
          </FormControl>
        </Stack>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button variant="contained" onClick={handleSave} disabled={codeId === "" || saving}>
          Create
        </Button>
      </DialogActions>
    </Dialog>
  );
}
