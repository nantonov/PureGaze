import type { TopicTranslate } from "@/entities/template/Topic.ts";

export interface TopicDetails {
  id: number;
  translates: TopicTranslate[];
}
