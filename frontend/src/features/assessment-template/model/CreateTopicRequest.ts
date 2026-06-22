import type { TopicTranslate } from "@/features/assessment-template/model/Topic.ts";

export interface CreateTopicRequest {
  templateId: number;
  translates: TopicTranslate[];
}
