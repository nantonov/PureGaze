import type { Translate } from "@/features/assessment-template/model/QuestionDetails.ts";

export interface CreateQuestionRequest {
  subTopicId: number;
  translates: Translate[];
  answer: { translates: Translate[] };
}
