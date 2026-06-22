import { BaseApi } from "@/shared/api/baseApi";
import type { SubtopicListItem } from "@/features/assessment-template/model/SubtopicListItem.ts";
import type { SubtopicDetails } from "@/features/assessment-template/model/SubtopicDetails.ts";
import type { CreateSubtopicRequest } from "@/features/assessment-template/model/CreateSubtopicRequest";
import type { UpdateSubtopicRequest } from "@/features/assessment-template/model/UpdateSubtopicRequest";

class SubtopicApi extends BaseApi {
  getForTopic(topicId: number): Promise<SubtopicListItem[]> {
    return this.get<SubtopicListItem[]>(`/topics/${topicId}/subtopics`);
  }

  getById(id: number): Promise<SubtopicDetails> {
    return this.get<SubtopicDetails>(`/subtopics/${id}`);
  }

  create(request: CreateSubtopicRequest): Promise<{ subtopicId: number }> {
    return this.post<{ subtopicId: number }>("/subtopics", request);
  }

  update(request: UpdateSubtopicRequest): Promise<void> {
    return this.put<void>("/subtopics", request);
  }

  deleteSubtopic(id: number): Promise<void> {
    return this.delete<void>(`/subtopics/${id}`);
  }
}

export const subtopicApi = new SubtopicApi();
