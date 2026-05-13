import { BaseApi } from "@/shared/api/baseApi";
import type { Topic } from "@/entities/template/Topic.ts";
import type { TopicDetails } from "@/entities/template/TopicDetails.ts";
import type { CreateTopicRequest } from "@/entities/template/CreateTopicRequest";
import type { UpdateTopicRequest } from "@/entities/template/UpdateTopicRequest";

interface GetTopicsResponse {
  topics: Topic[];
}

class TopicApi extends BaseApi {
  async getForTemplate(templateId: number): Promise<Topic[]> {
    const response = await this.get<GetTopicsResponse>(
      `/templates/${templateId}/topics?page=1&pageSize=1000`,
    );
    return response.topics;
  }

  getById(id: number): Promise<TopicDetails> {
    return this.get<TopicDetails>(`/topics/${id}`);
  }

  create(request: CreateTopicRequest): Promise<{ topicId: number }> {
    return this.post<{ topicId: number }>("/topics", request);
  }

  update(request: UpdateTopicRequest): Promise<void> {
    return this.put<void>("/topics", request);
  }

  deleteTopic(topicId: number): Promise<void> {
    return this.delete<void>(`/topics/${topicId}`);
  }
}

export const topicApi = new TopicApi();
