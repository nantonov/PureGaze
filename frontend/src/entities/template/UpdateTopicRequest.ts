import type { TopicTranslate } from "@/entities/template/Topic.ts";

export interface UpdateTopicRequest {
  topicId: number;
  translates: TopicTranslate[];
}
