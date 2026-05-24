export interface AssessmentStageItem {
  id: number;
  topicName: string;
  assessorFullName: string | null;
  isAssignedToCurrentUser: boolean;
}

export interface AssessmentListItem {
  id: number;
  employeeFullName: string;
  employeeEmail: string;
  gradeRange: string;
  isOwnAssessment: boolean;
  status: string;
  stages: AssessmentStageItem[];
}

export interface AssessmentHistoryItem {
  id: number;
  employeeFullName: string;
  employeeEmail: string;
  gradeRange: string;
  status: string;
  createdAt: string;
}
