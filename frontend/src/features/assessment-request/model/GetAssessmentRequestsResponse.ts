import type { AssessmentRequest } from "@/features/assessment-request/model/AssessmentRequest.ts";

export interface GetAssessmentRequestsResponse {
  total: number;
  assessmentRequests: AssessmentRequest[];
}
