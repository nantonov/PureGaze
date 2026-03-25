export type UpdateCodeRequest = {
    id: number;
    gradeId: string;
    toGradeId: string;
    display: string;
    totalEx: number;
    diffEx: number;
    levelVisionRu: string | null;
    levelVisionEn: string | null;
}
