export interface QuestionTranslate {
  language: string;
  content: string;
}

export interface QuestionItem {
  id: number;
  subTopicId: number;
  translates: QuestionTranslate[];
}
