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
import type { Code } from "@/entities/code/Code.ts";
import type { CreateCodeRequest } from "@/entities/code/CreateCodeRequest.ts";
import type { UpdateCodeRequest } from "@/entities/code/UpdateCodeRequest.ts";
import GradeSelect from "@/pages/codes/GradeSelect";
import { useCodeForm } from "@/pages/codes/useCodeForm";

interface Props {
    open: boolean;
    code: Code | null;
    onClose: () => void;
    onCreate: (req: CreateCodeRequest) => Promise<void>;
    onUpdate: (req: UpdateCodeRequest) => Promise<void>;
}

export default function CodeFormDialog({ open, code, onClose, onCreate, onUpdate }: Props) {
    const isEdit = code !== null;
    const { form, levels, handleChange, setField, diffExError, gradeError } = useCodeForm(code, open);

    const handleSave = async () => {
        if (diffExError || gradeError) return;
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
                    <TextField label="Name" value={form.name} onChange={handleChange("name")} />
                    <GradeSelect
                        label="From Grade *"
                        value={form.gradeId}
                        levels={levels}
                        onChange={(v) => setField("gradeId", v)}
                        error={!form.gradeId}
                    />
                    <GradeSelect
                        label="To Grade *"
                        value={form.toGradeId}
                        levels={levels}
                        onChange={(v) => setField("toGradeId", v)}
                        error={!form.toGradeId}
                    />
                    {gradeError && <FormHelperText error>{gradeError}</FormHelperText>}
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
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Cancel</Button>
                <Button variant="contained" onClick={handleSave} disabled={!!diffExError || !!gradeError}>
                    {isEdit ? "Save" : "Create"}
                </Button>
            </DialogActions>
        </Dialog>
    );
}
