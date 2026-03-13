import type {GetAssessment} from "@/types/AssessmentRequest/GetAssessment.ts";

export interface GetAssessmentResponse {
  total: number;
  assessmentRequests: GetAssessment[];
}