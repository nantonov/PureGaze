import { useState, useEffect } from "react";
import type { Code } from "@/types/Code/Code";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { ProfessionalLevel } from "@/types/ProfessionalLevel";
import { professionalLevelApi } from "@/api/professionalLevelApi";

const emptyForm: CreateCodeRequest = {
    gradeId: "",
    toGradeId: "",
    display: "",
    totalEx: 0,
    diffEx: 0,
    levelVisionRu: null,
    levelVisionEn: null,
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
            display: code.display,
            totalEx: code.totalEx,
            diffEx: code.diffEx,
            levelVisionRu: code.levelVisionRu,
            levelVisionEn: code.levelVisionEn,
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

    return { form, levels, handleChange, setField, diffExError };
}
