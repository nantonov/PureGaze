import { BaseApi } from "@/shared/api/baseApi";
import type { GetAssessmentHistoryQuery } from "@/features/assessment/model/GetAssessmentHistoryQuery";
import type { GetNewAssessmentsResponse } from "@/features/assessment/model/GetNewAssessmentsResponse";
import type { GetAssessmentHistoryResponse } from "@/features/assessment/model/GetAssessmentHistoryResponse";
import type { AssignedAssessmentStage } from "@/features/assessment/model/AssignedAssessmentStage";

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

  getAssignedToMe(): Promise<{ items: AssignedAssessmentStage[] }> {
    return this.get<{ items: AssignedAssessmentStage[] }>("/assessment-stages/assigned-to-me");
  }

  startStage(stageId: number): Promise<void> {
    return this.post<void>("/assessment-stages/start", { assessmentStageId: stageId });
  }

  scoreSubtopic(
    stageId: number,
    subtopicId: number,
    score: string,
    comment: string,
  ): Promise<void> {
    return this.post<void>("/subtopic-scores", { stageId, subtopicId, score, comment });
  }

  completeStage(stageId: number, isRecommended: boolean, summary: string): Promise<void> {
    return this.post<void>("/assessment-stages/complete", {
      assessmentStageId: stageId,
      isRecommended,
      summary,
    });
  }
}

export const assessmentApi = new AssessmentApi();
