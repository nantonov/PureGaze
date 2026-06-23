import type { SubtopicTranslate } from "@/features/assessment-template/model/SubtopicDetails.ts";

export interface CreateSubtopicRequest {
  topicId: number;
  translates: SubtopicTranslate[];
}
