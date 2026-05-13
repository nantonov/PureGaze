import type { SubtopicTranslate } from "@/entities/subtopic/SubtopicDetails.ts";

export interface UpdateSubtopicRequest {
  id: number;
  translates: SubtopicTranslate[];
}
