import { BaseApi } from "@/shared/api/baseApi";
import type { GetAssessmentHistoryQuery } from "@/features/assessment/model/GetAssessmentHistoryQuery";
import type { GetNewAssessmentsResponse } from "@/features/assessment/model/GetNewAssessmentsResponse";
import type { GetAssessmentHistoryResponse } from "@/features/assessment/model/GetAssessmentHistoryResponse";

class AssessmentApi extends BaseApi {
  private readonly baseUrl = "/assessments";

  getNew(): Promise<GetNewAssessmentsResponse> {
    return this.get<GetNewAssessmentsResponse>(`${this.baseUrl}/new`);
  }

  getHistory(query: GetAssessmentHistoryQuery): Promise<GetAssessmentHistoryResponse> {
    return this.post<GetAssessmentHistoryResponse>(`${this.baseUrl}/history`, query);
  }

  assignStage(stageId: number): Promise<void> {
    return this.post<void>("/assessment-stages/assign-me", { assessmentStageId: stageId });
  }

  unassignStage(stageId: number): Promise<void> {
    return this.post<void>("/assessment-stages/unassign-me", { assessmentStageId: stageId });
  }
}

export const assessmentApi = new AssessmentApi();
