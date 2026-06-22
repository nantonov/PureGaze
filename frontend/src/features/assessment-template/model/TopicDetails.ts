import type { TopicTranslate } from "@/features/assessment-template/model/Topic.ts";

export interface TopicDetails {
  id: number;
  translates: TopicTranslate[];
}
