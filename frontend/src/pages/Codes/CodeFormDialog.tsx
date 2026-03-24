import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
    Stack,
    FormHelperText,
} from "@mui/material";
import type { Code } from "@/types/Code/Code";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { UpdateCodeRequest } from "@/types/Code/UpdateCodeRequest";
import GradeSelect from "@/pages/Codes/GradeSelect";
import { useCodeForm } from "@/pages/Codes/useCodeForm";

interface Props {
    open: boolean;
    code: Code | null;
    onClose: () => void;
    onCreate: (req: CreateCodeRequest) => Promise<void>;
    onUpdate: (req: UpdateCodeRequest) => Promise<void>;
}

export default function CodeFormDialog({ open, code, onClose, onCreate, onUpdate }: Props) {
    const isEdit = code !== null;
    const { form, levels, handleChange, setField, diffExError } = useCodeForm(code, open);

    const handleSave = async () => {
        if (diffExError) return;
        if (isEdit) {
            await onUpdate({ ...form, id: code.id });
        } else {
            await onCreate(form);
        }
        onClose();
    };

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
            <DialogTitle>{isEdit ? "Edit Code" : "Create Code"}</DialogTitle>
            <DialogContent>
                <Stack spacing={2} sx={{ mt: 1 }}>
                    <TextField label="Display" value={form.display} onChange={handleChange("display")} />
                    <GradeSelect label="From Grade" value={form.gradeId} levels={levels} onChange={(v) => setField("gradeId", v)} />
                    <GradeSelect label="To Grade" value={form.toGradeId} levels={levels} onChange={(v) => setField("toGradeId", v)} />
                    <TextField label="Total Expirience" type="number" value={form.totalEx} onChange={handleChange("totalEx")} inputProps={{ min: 0, step: 1 }} />
                    <TextField
                        label="Diff Expirience"
                        type="number"
                        value={form.diffEx}
                        onChange={handleChange("diffEx")}
                        error={!!diffExError}
                        inputProps={{ min: 0, step: 1 }}
                    />
                    {diffExError && <FormHelperText error>{diffExError}</FormHelperText>}
                    <TextField label="Level Vision (RU)" value={form.levelVisionRu ?? ""} onChange={handleChange("levelVisionRu")} multiline rows={2} />
                    <TextField label="Level Vision (EN)" value={form.levelVisionEn ?? ""} onChange={handleChange("levelVisionEn")} multiline rows={2} />
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Cancel</Button>
                <Button variant="contained" onClick={handleSave} disabled={!!diffExError}>
                    {isEdit ? "Save" : "Create"}
                </Button>
            </DialogActions>
        </Dialog>
    );
}
