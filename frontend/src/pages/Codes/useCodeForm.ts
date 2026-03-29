import { useState, useEffect } from "react";
import type { Code } from "@/types/Code/Code";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { ProfessionalLevel } from "@/types/ProfessionalLevel";
import { professionalLevelApi } from "@/api/professionalLevelApi";

const emptyForm: CreateCodeRequest = {
    gradeId: "",
    toGradeId: "",
    name: "",
    totalEx: 0,
    diffEx: 0
};

const intFields: Set<string> = new Set(["totalEx", "diffEx"]);

export function useCodeForm(code: Code | null, open: boolean) {
    const [form, setForm] = useState<CreateCodeRequest>(emptyForm);
    const [levels, setLevels] = useState<ProfessionalLevel[]>([]);

    useEffect(() => {
        if (open) professionalLevelApi.getAll().then(setLevels);
    }, [open]);

    useEffect(() => {
        setForm(code ? {
            gradeId: code.gradeId,
            toGradeId: code.toGradeId,
            name: code.name,
            totalEx: code.totalEx,
            diffEx: code.diffEx,
        } : emptyForm);
    }, [code, open]);

    const handleChange = (field: keyof CreateCodeRequest) => (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = intFields.has(field) ? Number(e.target.value) : e.target.value;
        setForm((prev) => ({ ...prev, [field]: value }));
    };

    const setField = (field: keyof CreateCodeRequest, value: string) =>
        setForm((prev) => ({ ...prev, [field]: value }));

    const diffExError = Number(form.diffEx) > Number(form.totalEx)
        ? "Diff Experience cannot exceed Total Experience"
        : null;

    const gradeError = (() => {
        if (!form.gradeId) return "From Grade is required";
        if (!form.toGradeId) return "To Grade is required";
        const fromLevel = levels.find(l => l.id === form.gradeId);
        const toLevel = levels.find(l => l.id === form.toGradeId);
        if (fromLevel && toLevel) {
            const from = fromLevel.orderValue ?? 0;
            const to = toLevel.orderValue ?? 0;
            if (to <= from) return "To Grade must be strictly higher than From Grade";
            if (to !== from + 1) return "To Grade must be exactly one level above From Grade";
        }
        return null;
    })();

    return { form, levels, handleChange, setField, diffExError, gradeError };
}
