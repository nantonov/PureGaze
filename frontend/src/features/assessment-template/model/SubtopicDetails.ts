export interface SubtopicTranslate {
  language: string;
  name: string;
}

export interface SubtopicDetails {
  id: number;
  topicId: number;
  translates: SubtopicTranslate[];
}
