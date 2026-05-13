import type { Translate } from "@/entities/question/QuestionDetails.ts";

export interface CreateQuestionRequest {
  subTopicId: number;
  translates: Translate[];
  answer: { translates: Translate[] };
}
