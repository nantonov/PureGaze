import { BaseApi } from "@/shared/api/baseApi";
import type { Template } from "@/features/assessment-template/model/Template.ts";

interface GetTemplatesResponse {
  templates: Template[];
}

class TemplateApi extends BaseApi {
  private readonly baseUrl = "/templates";

  async getAll(): Promise<Template[]> {
    const response = await this.get<GetTemplatesResponse>(`${this.baseUrl}?page=1&pageSize=1000`);
    return response.templates;
  }

  create(codeId: number): Promise<{ templateId: number }> {
    return this.post<{ templateId: number }>(this.baseUrl, { codeId });
  }

  deleteTemplate(id: number): Promise<void> {
    return this.delete<void>(`${this.baseUrl}/${id}`);
  }
}

export const templateApi = new TemplateApi();
