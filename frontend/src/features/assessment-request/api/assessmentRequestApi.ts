import { BaseApi } from "@/shared/api/baseApi";
import type { GetAssessmentRequestsResponse } from "@/features/assessment-request/model/GetAssessmentRequestsResponse.ts";
import type { GetAssessmentRequestsQuery } from "@/features/assessment-request/model/GetAssessmentRequestsQuery.ts";

class AssessmentRequestApi extends BaseApi {
  private readonly baseUrl = "/assessment-requests";

  public async CreateAssessmentRequest(): Promise<void> {
    await this.post(`${this.baseUrl}`);
  }

  public async getAssignedToMe(
    query: GetAssessmentRequestsQuery,
  ): Promise<GetAssessmentRequestsResponse> {
    return this.post<GetAssessmentRequestsResponse>(`${this.baseUrl}/assigned-to-me`, query);
  }

  public async getMyRequests(
    query: GetAssessmentRequestsQuery,
  ): Promise<GetAssessmentRequestsResponse> {
    return this.post<GetAssessmentRequestsResponse>(`${this.baseUrl}/my`, query);
  }

  public async approve(id: number): Promise<void> {
    return this.post<void>(`${this.baseUrl}/approve`, { id });
  }

  public async reject(id: number, reason?: string): Promise<void> {
    return this.post<void>(`${this.baseUrl}/reject`, { id, reason: reason });
  }
}

export const assessmentRequestApi = new AssessmentRequestApi();
