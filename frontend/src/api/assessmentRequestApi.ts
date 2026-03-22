import { BaseApi } from "./baseApi";
import type { GetAssessmentResponse } from "@/types/AssessmentRequest/GetAssessmentResponse.ts";
import type { GetAssessmentRequest } from "@/types/AssessmentRequest/GetAssessmentRequest.ts";

class AssessmentRequestApi extends BaseApi {
  private readonly baseUrl = "/assessment-requests";

  public async CreateAssessmentRequest(): Promise<void> {
   await this.post(`${this.baseUrl}`);
  }
  
  public async getAssignedToMe(request: GetAssessmentRequest): Promise<GetAssessmentResponse> {
    return this.post<GetAssessmentResponse>(`${this.baseUrl}/assigned-to-me`, request);
  }

  public async getMyRequests(request: GetAssessmentRequest): Promise<GetAssessmentResponse> {
    return this.post<GetAssessmentResponse>(`${this.baseUrl}/my`, request);
  }
}

export const assessmentRequestApi = new AssessmentRequestApi();
