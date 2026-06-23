import type { AssessmentHistoryItem } from "./Assessment";

export interface GetAssessmentHistoryResponse {
  total: number;
  items: AssessmentHistoryItem[];
}
