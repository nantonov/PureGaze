export interface Translate {
  language: string;
  content: string;
}

export interface QuestionAnswer {
  id: number;
  questionId: number;
  translates: Translate[];
}

export interface QuestionDetails {
  id: number;
  subTopicId: number;
  translates: Translate[];
  answer: QuestionAnswer | null;
}
