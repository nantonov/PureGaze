import type { TopicTranslate } from "@/entities/template/Topic.ts";

export interface CreateTopicRequest {
  templateId: number;
  translates: TopicTranslate[];
}
