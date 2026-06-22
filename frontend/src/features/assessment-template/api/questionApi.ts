import { BaseApi } from "@/shared/api/baseApi";
import type { QuestionItem } from "@/features/assessment-template/model/QuestionItem.ts";
import type { QuestionDetails } from "@/features/assessment-template/model/QuestionDetails.ts";
import type { CreateQuestionRequest } from "@/features/assessment-template/model/CreateQuestionRequest";
import type { UpdateQuestionRequest } from "@/features/assessment-template/model/UpdateQuestionRequest";

class QuestionApi extends BaseApi {
  getBySubtopic(subtopicId: number): Promise<QuestionItem[]> {
    return this.get<QuestionItem[]>(`/questions/subtopic/${subtopicId}`);
  }

  getById(id: number): Promise<QuestionDetails> {
    return this.get<QuestionDetails>(`/questions/${id}`);
  }

  create(req: CreateQuestionRequest): Promise<void> {
    return this.post<void>("/questions", req);
  }

  update(req: UpdateQuestionRequest): Promise<void> {
    return this.put<void>("/questions", req);
  }

  deleteQuestion(id: number): Promise<void> {
    return this.delete<void>(`/questions/${id}`);
  }
}

export const questionApi = new QuestionApi();
