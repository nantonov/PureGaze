import type { AssessmentRequest } from "@/entities/assessmentRequest/AssessmentRequest.ts";

export interface GetAssessmentRequestsResponse {
  total: number;
  assessmentRequests: AssessmentRequest[];
}
