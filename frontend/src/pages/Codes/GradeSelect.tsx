import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import type { ProfessionalLevel } from "@/types/ProfessionalLevel";

interface Props {
    label: string;
    value: string;
    levels: ProfessionalLevel[];
    onChange: (value: string) => void;
}

export default function GradeSelect({ label, value, levels, onChange }: Props) {
    return (
        <FormControl fullWidth>
            <InputLabel>{label}</InputLabel>
            <Select label={label} value={value} onChange={(e) => onChange(e.target.value)}>
                {levels.map((l) => (
                    <MenuItem key={l.id} value={l.id}>{l.value}</MenuItem>
                ))}
            </Select>
        </FormControl>
    );
}
