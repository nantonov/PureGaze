export type AssessmentMark = "NeedsImprovement" | "Competent" | "Excellent";

export interface AssignedQuestion {
  id: number;
  content: string;
  hint: string | null;
}

export interface AssignedSubtopic {
  id: number;
  name: string;
  questions: AssignedQuestion[];
  score: AssessmentMark | null;
  comment: string | null;
}

export interface AssignedAssessmentStage {
  id: number;
  assessmentId: number;
  employeeFullName: string;
  employeeEmail: string;
  topicName: string;
  status: "Pending" | "InProgress";
  subtopics: AssignedSubtopic[];
}
