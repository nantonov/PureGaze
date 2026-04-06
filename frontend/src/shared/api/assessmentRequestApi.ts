import { BaseApi } from "./baseApi";
import type { GetAssessmentRequestsResponse } from "@/entities/assessmentRequest/GetAssessmentRequestsResponse.ts";
import type { GetAssessmentRequestsQuery } from "@/entities/assessmentRequest/GetAssessmentRequestsQuery.ts";

class AssessmentRequestApi extends BaseApi {
  private readonly baseUrl = "/assessment-requests";

  public async CreateAssessmentRequest(): Promise<void> {
   await this.post(`${this.baseUrl}`);
  }
  
  public async getAssignedToMe(query: GetAssessmentRequestsQuery): Promise<GetAssessmentRequestsResponse> {
    return this.post<GetAssessmentRequestsResponse>(`${this.baseUrl}/assigned-to-me`, query);
  }

  public async getMyRequests(query: GetAssessmentRequestsQuery): Promise<GetAssessmentRequestsResponse> {
    return this.post<GetAssessmentRequestsResponse>(`${this.baseUrl}/my`, query);
  }
}

export const assessmentRequestApi = new AssessmentRequestApi();
