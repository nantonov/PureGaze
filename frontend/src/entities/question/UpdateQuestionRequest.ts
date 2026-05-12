import type { Translate } from "@/entities/question/QuestionDetails.ts";

export interface UpdateQuestionRequest {
  id: number;
  translates: Translate[];
  answer: { translates: Translate[] };
}
