import { Box, Stack, Tab, Tabs, Typography } from "@mui/material";
import { useCallback, useEffect, useMemo, useState } from "react";
import type { Template } from "@/entities/template/Template.ts";
import type { Topic } from "@/entities/template/Topic.ts";
import type { SubtopicListItem } from "@/entities/subtopic/SubtopicListItem.ts";
import type { QuestionItem } from "@/entities/question/QuestionItem.ts";
import type { Translate } from "@/entities/question/QuestionDetails.ts";
import type { SubtopicTranslate } from "@/entities/subtopic/SubtopicDetails.ts";
import type { TopicTranslate } from "@/entities/template/Topic.ts";
import { templateApi } from "@/shared/api/templateApi.ts";
import { topicApi } from "@/shared/api/topicApi.ts";
import { subtopicApi } from "@/shared/api/subtopicApi.ts";
import { questionApi } from "@/shared/api/questionApi.ts";
import PageContentBox from "@/widgets/tableBox/PageContentBox.tsx";
import TemplateListPanel from "@/pages/assessmentTemplates/components/TemplateListPanel.tsx";
import TemplateDetailHeader from "@/pages/assessmentTemplates/components/TemplateDetailHeader.tsx";
import GeneralTab from "@/pages/assessmentTemplates/tabs/GeneralTab.tsx";
import TopicsTab, { type TopicRow } from "@/pages/assessmentTemplates/tabs/TopicsTab.tsx";
import SubtopicsTab, { type SubtopicRow } from "@/pages/assessmentTemplates/tabs/SubtopicsTab.tsx";
import QuestionsTab, { type QuestionRow } from "@/pages/assessmentTemplates/tabs/QuestionsTab.tsx";
import AssessmentTemplateDialogs from "@/pages/assessmentTemplates/dialogs/AssessmentTemplateDialogs.tsx";

type TabKey = "general" | "topics" | "subtopics" | "questions";

interface SubtopicsByTopic {
  [topicId: number]: SubtopicListItem[];
}

interface QuestionsBySubtopic {
  [subtopicId: number]: QuestionItem[];
}

export default function AssessmentTemplatesPage() {
  // Master data
  const [templates, setTemplates] = useState<Template[]>([]);
  const [selectedTemplateId, setSelectedTemplateId] = useState<number | null>(null);

  // Detail data
  const [topics, setTopics] = useState<Topic[]>([]);
  const [subtopicsByTopic, setSubtopicsByTopic] = useState<SubtopicsByTopic>({});
  const [detailLoading, setDetailLoading] = useState(false);

  // Questions data (lazy-loaded when Questions tab is first opened)
  const [questionsBySubtopic, setQuestionsBySubtopic] = useState<QuestionsBySubtopic>({});
  const [questionsLoading, setQuestionsLoading] = useState(false);
  const [questionsLoaded, setQuestionsLoaded] = useState(false);

  const [activeTab, setActiveTab] = useState<TabKey>("general");
  const [subtopicsTopicFilter, setSubtopicsTopicFilter] = useState<number | "">("");
  const [questionsTopicFilter, setQuestionsTopicFilter] = useState<number | "">("");
  const [questionsSubtopicFilter, setQuestionsSubtopicFilter] = useState<number | "">("");

  // Dialog state
  const [templateDialogOpen, setTemplateDialogOpen] = useState(false);
  const [topicDialogOpen, setTopicDialogOpen] = useState(false);
  const [editTopic, setEditTopic] = useState<Topic | null>(null);
  const [subtopicDialogOpen, setSubtopicDialogOpen] = useState(false);
  const [editSubtopicId, setEditSubtopicId] = useState<number | null>(null);
  const [subtopicCreateContext, setSubtopicCreateContext] = useState<
    "pick-topic" | "fixed-topic" | null
  >(null);
  const [fixedTopicId, setFixedTopicId] = useState<number | null>(null);
  const [questionDetailId, setQuestionDetailId] = useState<number | null>(null);
  const [questionFormOpen, setQuestionFormOpen] = useState(false);
  const [editQuestionId, setEditQuestionId] = useState<number | null>(null);
  const [questionCreateSubtopicId, setQuestionCreateSubtopicId] = useState<number | null>(null);

  const [templateDeleteConfirm, setTemplateDeleteConfirm] = useState(false);

  // ---- Initial load
  useEffect(() => {
    templateApi.getAll().then((list) => {
      setTemplates(list);
      if (list.length > 0) {
        setSelectedTemplateId((prev) => prev ?? list[0].id);
      }
    });
  }, []);

  // ---- Load topics
  const loadDetail = useCallback(async (templateId: number) => {
    setDetailLoading(true);
    try {
      const topicsList = await topicApi.getForTemplate(templateId);
      const subtopicsResults = await Promise.all(
        topicsList.map((t) =>
          subtopicApi.getForTopic(t.id).then((items) => [t.id, items] as const),
        ),
      );
      const grouped: SubtopicsByTopic = {};
      for (const [topicId, items] of subtopicsResults) grouped[topicId] = items;
      setTopics(topicsList);
      setSubtopicsByTopic(grouped);
    } finally {
      setDetailLoading(false);
    }
  }, []);

  useEffect(() => {
    setActiveTab("general");
    setSubtopicsTopicFilter("");
    setQuestionsTopicFilter("");
    setQuestionsSubtopicFilter("");
    setQuestionsLoaded(false);
    setQuestionsBySubtopic({});
    setTopics([]);
    setSubtopicsByTopic({});
    if (selectedTemplateId === null) return;
    loadDetail(selectedTemplateId);
  }, [selectedTemplateId, loadDetail]);

  // ---- Load questions (lazy)
  const loadQuestions = useCallback(async (allSubtopicIds: number[]) => {
    if (allSubtopicIds.length === 0) return;
    setQuestionsLoading(true);
    try {
      const results = await Promise.all(
        allSubtopicIds.map((sid) =>
          questionApi.getBySubtopic(sid).then((items) => [sid, items] as const),
        ),
      );
      const grouped: QuestionsBySubtopic = {};
      for (const [sid, items] of results) grouped[sid] = items;
      setQuestionsBySubtopic(grouped);
      setQuestionsLoaded(true);
    } finally {
      setQuestionsLoading(false);
    }
  }, []);

  const allSubtopicIds = useMemo(
    () =>
      Object.values(subtopicsByTopic)
        .flat()
        .map((s) => s.id),
    [subtopicsByTopic],
  );

  useEffect(() => {
    if (activeTab === "questions" && !questionsLoaded && allSubtopicIds.length > 0) {
      loadQuestions(allSubtopicIds);
    }
  }, [activeTab, questionsLoaded, allSubtopicIds, loadQuestions]);

  // ---- Derived data
  const selectedTemplate = useMemo(
    () => templates.find((t) => t.id === selectedTemplateId) ?? null,
    [templates, selectedTemplateId],
  );

  const topicRows: TopicRow[] = useMemo(
    () =>
      topics.map((t) => ({
        id: t.id,
        name: t.name ?? `#${t.id}`,
        subtopicCount: subtopicsByTopic[t.id]?.length ?? 0,
      })),
    [topics, subtopicsByTopic],
  );

  const subtopicRows: SubtopicRow[] = useMemo(() => {
    const rows: SubtopicRow[] = [];
    for (const topic of topics) {
      for (const s of subtopicsByTopic[topic.id] ?? []) {
        rows.push({
          id: s.id,
          name: s.name ?? `#${s.id}`,
          topicId: topic.id,
          topicName: topic.name ?? `#${topic.id}`,
          questionCount: questionsLoaded ? (questionsBySubtopic[s.id]?.length ?? 0) : null,
        });
      }
    }
    return rows;
  }, [topics, subtopicsByTopic, questionsLoaded, questionsBySubtopic]);

  const questionRows: QuestionRow[] = useMemo(() => {
    const rows: QuestionRow[] = [];
    for (const topic of topics) {
      for (const subtopic of subtopicsByTopic[topic.id] ?? []) {
        for (const q of questionsBySubtopic[subtopic.id] ?? []) {
          const firstTranslate = q.translates[0]?.content ?? `#${q.id}`;
          rows.push({
            id: q.id,
            content: firstTranslate,
            subtopicId: subtopic.id,
            subtopicName: subtopic.name ?? `#${subtopic.id}`,
            topicId: topic.id,
            topicName: topic.name ?? `#${topic.id}`,
          });
        }
      }
    }
    return rows;
  }, [topics, subtopicsByTopic, questionsBySubtopic]);

  const topicOptions = useMemo(
    () => topics.map((t) => ({ id: t.id, name: t.name ?? `#${t.id}` })),
    [topics],
  );

  const subtopicOptions = useMemo(
    () =>
      subtopicRows.map((s) => ({
        id: s.id,
        name: s.name,
        topicId: s.topicId,
        topicName: s.topicName,
      })),
    [subtopicRows],
  );

  // ---- Handlers: Template
  const handleCreateTemplate = async (codeId: number) => {
    const result = await templateApi.create(codeId);
    const fresh = await templateApi.getAll();
    setTemplates(fresh);
    setSelectedTemplateId(result.templateId);
  };

  const handleDeleteSelectedTemplate = async () => {
    if (selectedTemplateId === null) return;
    await templateApi.deleteTemplate(selectedTemplateId);
    const remaining = templates.filter((t) => t.id !== selectedTemplateId);
    setTemplates(remaining);
    setSelectedTemplateId(remaining[0]?.id ?? null);
  };

  const handleDeleteTemplateById = async (id: number) => {
    await templateApi.deleteTemplate(id);
    const remaining = templates.filter((t) => t.id !== id);
    setTemplates(remaining);
    if (selectedTemplateId === id) setSelectedTemplateId(remaining[0]?.id ?? null);
  };

  // ---- Handlers: Topic
  const handleCreateTopic = async (translates: TopicTranslate[]) => {
    if (selectedTemplateId === null) return;
    await topicApi.create({ templateId: selectedTemplateId, translates });
    await loadDetail(selectedTemplateId);
  };

  const handleUpdateTopic = async (topicId: number, translates: TopicTranslate[]) => {
    await topicApi.update({ topicId, translates });
    if (selectedTemplateId !== null) await loadDetail(selectedTemplateId);
  };

  const handleDeleteTopic = async (topicId: number) => {
    await topicApi.deleteTopic(topicId);
    if (selectedTemplateId !== null) await loadDetail(selectedTemplateId);
  };

  // ---- Handlers: Subtopic
  const handleCreateSubtopic = async (topicId: number, translates: SubtopicTranslate[]) => {
    await subtopicApi.create({ topicId, translates });
    if (selectedTemplateId !== null) await loadDetail(selectedTemplateId);
  };

  const handleUpdateSubtopic = async (subtopicId: number, translates: SubtopicTranslate[]) => {
    await subtopicApi.update({ id: subtopicId, translates });
    if (selectedTemplateId !== null) await loadDetail(selectedTemplateId);
  };

  const handleDeleteSubtopic = async (subtopicId: number) => {
    await subtopicApi.deleteSubtopic(subtopicId);
    if (selectedTemplateId !== null) await loadDetail(selectedTemplateId);
  };

  // ---- Handlers: Question
  const reloadQuestions = async () => {
    if (allSubtopicIds.length > 0) await loadQuestions(allSubtopicIds);
  };

  const handleCreateQuestion = async (
    subtopicId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => {
    await questionApi.create({
      subTopicId: subtopicId,
      translates,
      answer: { translates: answerTranslates },
    });
    await reloadQuestions();
  };

  const handleUpdateQuestion = async (
    questionId: number,
    translates: Translate[],
    answerTranslates: Translate[],
  ) => {
    await questionApi.update({
      id: questionId,
      translates,
      answer: { translates: answerTranslates },
    });
    await reloadQuestions();
  };

  const handleDeleteQuestion = async (questionId: number) => {
    await questionApi.deleteQuestion(questionId);
    setQuestionsBySubtopic((prev) => {
      const next = { ...prev };
      for (const sid of Object.keys(next)) {
        next[+sid] = next[+sid].filter((q) => q.id !== questionId);
      }
      return next;
    });
  };

  // ---- UI helpers
  const totalSubtopics = subtopicRows.length;
  const totalQuestions = questionRows.length;

  return (
    <PageContentBox>
      <Box
        sx={{
          flex: 1,
          minHeight: 0,
          display: "flex",
          borderRadius: 2,
          border: "1px solid var(--border-color, rgba(0,0,0,0.12))",
          overflow: "hidden",
          bgcolor: "background.default",
        }}
      >
        <TemplateListPanel
          templates={templates}
          selectedId={selectedTemplateId}
          onSelect={setSelectedTemplateId}
          onCreate={() => setTemplateDialogOpen(true)}
          onDelete={handleDeleteTemplateById}
        />

        <Stack sx={{ flex: 1, minWidth: 0, p: 3 }} spacing={1.5}>
          {selectedTemplate === null ? (
            <Stack
              alignItems="center"
              justifyContent="center"
              sx={{ flex: 1, color: "text.secondary" }}
              spacing={1}
            >
              <Typography variant="h6">Select a template</Typography>
              <Typography variant="body2">
                Pick a template on the left, or create one to get started.
              </Typography>
            </Stack>
          ) : (
            <>
              <TemplateDetailHeader
                template={selectedTemplate}
                topicsCount={topics.length}
                subtopicsCount={totalSubtopics}
                questionsCount={questionsLoaded ? totalQuestions : null}
              />

              {/* Tabs */}
              <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
                <Tabs
                  value={activeTab}
                  onChange={(_, v: TabKey) => setActiveTab(v)}
                  sx={{
                    "& .MuiTab-root": { textTransform: "none", fontWeight: 500 },
                    "& .Mui-selected": { color: "var(--brand-color) !important" },
                    "& .MuiTabs-indicator": { backgroundColor: "var(--brand-color)" },
                  }}
                >
                  <Tab label="General" value="general" />
                  <Tab label={`Topics (${topics.length})`} value="topics" />
                  <Tab label={`Subtopics (${totalSubtopics})`} value="subtopics" />
                  <Tab
                    label={questionsLoaded ? `Questions (${totalQuestions})` : "Questions"}
                    value="questions"
                  />
                </Tabs>
              </Box>

              {/* Tab content */}
              {activeTab === "general" && (
                <GeneralTab
                  topics={topicOptions}
                  subtopics={subtopicRows}
                  questions={questionRows}
                  questionsLoaded={questionsLoaded}
                  questionsLoading={questionsLoading}
                  onLoadQuestions={reloadQuestions}
                  onTopicClick={(topicId) => {
                    setSubtopicsTopicFilter(topicId);
                    setActiveTab("subtopics");
                  }}
                  onSubtopicClick={(subtopicId) => {
                    setQuestionsSubtopicFilter(subtopicId);
                    setActiveTab("questions");
                  }}
                  onQuestionClick={(questionId, subtopicId) => {
                    setQuestionsSubtopicFilter(subtopicId);
                    setActiveTab("questions");
                    setQuestionDetailId(questionId);
                  }}
                />
              )}

              {activeTab === "topics" && (
                <TopicsTab
                  rows={topicRows}
                  loading={detailLoading}
                  onAdd={() => {
                    setEditTopic(null);
                    setTopicDialogOpen(true);
                  }}
                  onEdit={(topic) => {
                    setEditTopic(topic);
                    setTopicDialogOpen(true);
                  }}
                  onDelete={handleDeleteTopic}
                  onSelect={(topicId) => {
                    setSubtopicsTopicFilter(topicId);
                    setActiveTab("subtopics");
                  }}
                />
              )}

              {activeTab === "subtopics" && (
                <SubtopicsTab
                  rows={subtopicRows}
                  topics={topicOptions}
                  loading={detailLoading}
                  topicFilter={subtopicsTopicFilter}
                  onTopicFilterChange={setSubtopicsTopicFilter}
                  onAdd={() => {
                    setEditSubtopicId(null);
                    setFixedTopicId(
                      subtopicsTopicFilter !== "" ? (subtopicsTopicFilter as number) : null,
                    );
                    setSubtopicCreateContext("pick-topic");
                    setSubtopicDialogOpen(true);
                  }}
                  onEdit={(subtopicId) => {
                    setEditSubtopicId(subtopicId);
                    setSubtopicCreateContext(null);
                    setSubtopicDialogOpen(true);
                  }}
                  onDelete={handleDeleteSubtopic}
                  onSelect={(subtopicId) => {
                    setQuestionsSubtopicFilter(subtopicId);
                    setActiveTab("questions");
                  }}
                />
              )}

              {activeTab === "questions" && (
                <QuestionsTab
                  rows={questionRows}
                  topics={topicOptions}
                  subtopics={subtopicOptions}
                  loading={questionsLoading}
                  topicFilter={questionsTopicFilter}
                  onTopicFilterChange={setQuestionsTopicFilter}
                  subtopicFilter={questionsSubtopicFilter}
                  onSubtopicFilterChange={setQuestionsSubtopicFilter}
                  onRowClick={(questionId) => setQuestionDetailId(questionId)}
                  onAdd={() => {
                    setEditQuestionId(null);
                    setQuestionCreateSubtopicId(
                      questionsSubtopicFilter !== "" ? (questionsSubtopicFilter as number) : null,
                    );
                    setQuestionFormOpen(true);
                  }}
                  onEdit={(questionId) => {
                    setEditQuestionId(questionId);
                    setQuestionFormOpen(true);
                  }}
                  onDelete={handleDeleteQuestion}
                />
              )}
            </>
          )}
        </Stack>
      </Box>

      <AssessmentTemplateDialogs
        templateDialogOpen={templateDialogOpen}
        onTemplateDialogClose={() => setTemplateDialogOpen(false)}
        onCreateTemplate={handleCreateTemplate}
        topicDialogOpen={topicDialogOpen}
        editTopic={editTopic}
        onTopicDialogClose={() => setTopicDialogOpen(false)}
        onCreateTopic={handleCreateTopic}
        onUpdateTopic={handleUpdateTopic}
        subtopicDialogOpen={subtopicDialogOpen}
        editSubtopicId={editSubtopicId}
        subtopicTopics={subtopicCreateContext === "pick-topic" ? topicOptions : undefined}
        fixedTopicId={fixedTopicId ?? undefined}
        onSubtopicDialogClose={() => {
          setSubtopicDialogOpen(false);
          setSubtopicCreateContext(null);
          setFixedTopicId(null);
        }}
        onCreateSubtopic={handleCreateSubtopic}
        onUpdateSubtopic={handleUpdateSubtopic}
        questionDetailId={questionDetailId}
        onQuestionDetailClose={() => setQuestionDetailId(null)}
        onEditQuestionFromDetail={(id) => {
          setQuestionDetailId(null);
          setEditQuestionId(id);
          setQuestionFormOpen(true);
        }}
        questionFormOpen={questionFormOpen}
        editQuestionId={editQuestionId}
        questionCreateSubtopicId={questionCreateSubtopicId ?? undefined}
        questionSubtopics={editQuestionId === null ? subtopicOptions : undefined}
        onQuestionFormClose={() => {
          setQuestionFormOpen(false);
          setEditQuestionId(null);
          setQuestionCreateSubtopicId(null);
        }}
        onCreateQuestion={handleCreateQuestion}
        onUpdateQuestion={handleUpdateQuestion}
        templateDeleteConfirm={templateDeleteConfirm}
        onTemplateDeleteCancel={() => setTemplateDeleteConfirm(false)}
        onTemplateDeleteConfirm={() => {
          setTemplateDeleteConfirm(false);
          handleDeleteSelectedTemplate();
        }}
      />
    </PageContentBox>
  );
}
