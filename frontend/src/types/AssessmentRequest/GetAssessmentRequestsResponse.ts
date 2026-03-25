import type { AssessmentRequest } from "@/types/AssessmentRequest/AssessmentRequest.ts";

export interface GetAssessmentRequestsResponse {
  total: number;
  assessmentRequests: AssessmentRequest[];
}