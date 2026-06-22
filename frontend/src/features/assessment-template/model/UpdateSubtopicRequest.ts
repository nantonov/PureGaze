import type { SubtopicTranslate } from "@/features/assessment-template/model/SubtopicDetails.ts";

export interface UpdateSubtopicRequest {
  id: number;
  translates: SubtopicTranslate[];
}
