export interface UpdateCodeRequest {
    id: number;
    gradeId: string;
    toGradeId: string;
    name: string;
    totalEx: number;
    diffEx: number;
}
