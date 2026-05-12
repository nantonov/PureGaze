import type { Topic } from "@/entities/template/Topic.ts";
import type { TopicTranslate } from "@/entities/template/Topic.ts";
import type { SubtopicTranslate } from "@/entities/subtopic/SubtopicDetails.ts";
import type { Translate } from "@/entities/question/QuestionDetails.ts";
import { ConfirmDeleteDialog } from "@/shared/ui/ConfirmDeleteDialog.tsx";
import TemplateFormDialog from "./TemplateFormDialog.tsx";
import TopicFormDialog from "./TopicFormDialog.tsx";
import SubtopicFormDialog from "./SubtopicFormDialog.tsx";
import QuestionDetailDialog from "./QuestionDetailDialog.tsx";
import QuestionFormDialog from "./QuestionFormDialog.tsx";

interface Props {
  // Template
  templateDialogOpen: boolean;
  onTemplateDialogClose: () => void;
  onCreateTemplate: (codeId: number) => Promise<void>;

  // Topic
  topicDialogOpen: boolean;
  editTopic: Topic | null;
  onTopicDialogClose: () => void;
  onCreateTopic: (translates: TopicTranslate[]) => Promise<void>;
  onUpdateTopic: (topicId: number, translates: TopicTranslate[]) => Promise<void>;

  // Subtopic
  subtopicDialogOpen: boolean;
  editSubtopicId: number | null;
  subtopicTopics: { id: number; name: string }[] | undefined;
  fixedTopicId: number | undefined;
  onSubtopicDialogClose: () => void;
  onCreateSubtopic: (topicId: number, translates: SubtopicTranslate[]) => Promise<void>;
  onUpdateSubtopic: (subtopicId: number, translates: SubtopicTranslate[]) => Promise<void>;

  // Question detail
  questionDetailId: number | null;
  onQuestionDetailClose: () => void;
  onEditQuestionFromDetail: (questionId: number) => void;

  // Question form
  questionFormOpen: boolean;
  editQuestionId: number | null;
  questionCreateSubtopicId: number | undefined;
  onQuestionFormClose: () => void;
  onCreateQuestion: (
    subtopicId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => Promise<void>;
  onUpdateQuestion: (
    questionId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => Promise<void>;

  // Template delete confirm
  templateDeleteConfirm: boolean;
  onTemplateDeleteCancel: () => void;
  onTemplateDeleteConfirm: () => void;
}

export default function AssessmentTemplateDialogs({
  templateDialogOpen,
  onTemplateDialogClose,
  onCreateTemplate,
  topicDialogOpen,
  editTopic,
  onTopicDialogClose,
  onCreateTopic,
  onUpdateTopic,
  subtopicDialogOpen,
  editSubtopicId,
  subtopicTopics,
  fixedTopicId,
  onSubtopicDialogClose,
  onCreateSubtopic,
  onUpdateSubtopic,
  questionDetailId,
  onQuestionDetailClose,
  onEditQuestionFromDetail,
  questionFormOpen,
  editQuestionId,
  questionCreateSubtopicId,
  onQuestionFormClose,
  onCreateQuestion,
  onUpdateQuestion,
  templateDeleteConfirm,
  onTemplateDeleteCancel,
  onTemplateDeleteConfirm,
}: Props) {
  return (
    <>
      <TemplateFormDialog
        open={templateDialogOpen}
        onClose={onTemplateDialogClose}
        onCreate={onCreateTemplate}
      />
      <TopicFormDialog
        open={topicDialogOpen}
        topic={editTopic}
        onClose={onTopicDialogClose}
        onCreate={onCreateTopic}
        onUpdate={onUpdateTopic}
      />
      <SubtopicFormDialog
        open={subtopicDialogOpen}
        subtopicId={editSubtopicId}
        topics={subtopicTopics}
        defaultTopicId={fixedTopicId}
        onClose={onSubtopicDialogClose}
        onCreate={onCreateSubtopic}
        onUpdate={onUpdateSubtopic}
      />
      <QuestionDetailDialog
        questionId={questionDetailId}
        onClose={onQuestionDetailClose}
        onEdit={onEditQuestionFromDetail}
      />
      <QuestionFormDialog
        open={questionFormOpen}
        questionId={editQuestionId}
        defaultSubtopicId={questionCreateSubtopicId}
        onClose={onQuestionFormClose}
        onCreate={onCreateQuestion}
        onUpdate={onUpdateQuestion}
      />
      <ConfirmDeleteDialog
        open={templateDeleteConfirm}
        label="Template"
        onCancel={onTemplateDeleteCancel}
        onConfirm={onTemplateDeleteConfirm}
      />
    </>
  );
}
