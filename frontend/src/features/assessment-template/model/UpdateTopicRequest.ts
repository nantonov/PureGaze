import type { TopicTranslate } from "@/features/assessment-template/model/Topic.ts";

export interface UpdateTopicRequest {
  topicId: number;
  translates: TopicTranslate[];
}
