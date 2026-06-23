import type { Translate } from "@/features/assessment-template/model/QuestionDetails.ts";

export interface UpdateQuestionRequest {
  id: number;
  translates: Translate[];
  answer: { translates: Translate[] };
}
