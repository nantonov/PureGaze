using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedAssessmentStages;

public sealed record GetAssignedAssessmentStagesResult(IReadOnlyList<AssignedAssessmentStageDto> Items);

public sealed record AssignedAssessmentStageDto(
    int Id,
    int AssessmentId,
    string EmployeeFullName,
    string EmployeeEmail,
    string TopicName,
    string Status,
    IReadOnlyList<AssignedSubtopicDto> Subtopics)
{
    public static AssignedAssessmentStageDto FromEntity(AssessmentStage stage) => new(
        stage.Id,
        stage.AssessmentId,
        $"{stage.Assessment?.Employee?.FirstNameEn} {stage.Assessment?.Employee?.LastNameEn}".Trim(),
        stage.Assessment?.Employee?.Email ?? string.Empty,
        stage.Topic?.TopicTranslates.OrderBy(x => x.Language).FirstOrDefault()?.Name ?? string.Empty,
        stage.Status.ToString(),
        stage.Topic?.Subtopics.OrderBy(x => x.Id).Select(subtopic =>
        {
            var saved = stage.Scores.FirstOrDefault(x => x.SubtopicId == subtopic.Id);
            return new AssignedSubtopicDto(
                subtopic.Id,
                subtopic.SubtopicTranslates.OrderBy(x => x.Language).FirstOrDefault()?.Name ?? string.Empty,
                subtopic.Questions.OrderBy(x => x.Id).Select(question => new AssignedQuestionDto(
                    question.Id,
                    question.QuestionTranslates.OrderBy(x => x.Language).FirstOrDefault()?.Content ?? string.Empty,
                    question.Answer?.AnswerTranslates.OrderBy(x => x.Language).FirstOrDefault()?.Content)).ToList(),
                saved?.Score.ToString(),
                saved?.Comment);
        }).ToList() ?? []);
}

public sealed record AssignedSubtopicDto(
    int Id,
    string Name,
    IReadOnlyList<AssignedQuestionDto> Questions,
    string? Score,
    string? Comment);

public sealed record AssignedQuestionDto(int Id, string Content, string? Hint);
