import type { SubtopicTranslate } from "@/entities/subtopic/SubtopicDetails.ts";

export interface CreateSubtopicRequest {
  topicId: number;
  translates: SubtopicTranslate[];
}
