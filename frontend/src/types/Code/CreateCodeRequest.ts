export type CreateCodeRequest = {
    gradeId: string;
    toGradeId: string;
    display: string;
    totalEx: number;
    diffEx: number;
    levelVisionRu: string | null;
    levelVisionEn: string | null;
}
