import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import type { ProfessionalLevel } from "@/entities/ProfessionalLevel";

interface Props {
    label: string;
    value: string;
    levels: ProfessionalLevel[];
    onChange: (value: string) => void;
    error?: boolean;
}

export default function GradeSelect({ label, value, levels, onChange, error }: Props) {
    return (
        <FormControl fullWidth error={error}>
            <InputLabel>{label}</InputLabel>
            <Select label={label} value={value} onChange={(e) => onChange(e.target.value)}>
                {levels.map((l) => (
                    <MenuItem key={l.id} value={l.id}>{l.value}</MenuItem>
                ))}
            </Select>
        </FormControl>
    );
}
