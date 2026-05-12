using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Subtopics.CreateSubtopic;

public sealed record CreateSubtopicCommand(
    int TopicId,
    List<SubtopicTranslateDto> Translates,
    List<CreateQuestionDto>? Questions = null) : IRequest<CreateSubtopicResult>;

public sealed record SubtopicTranslateDto(Language Language, string Name);

public sealed record CreateQuestionDto(
    List<QuestionTranslateDto> Translates,
    CreateAnswerDto Answer);

public sealed record QuestionTranslateDto(Language Language, string Content);

public sealed record CreateAnswerDto(List<AnswerTranslateDto> Translates);

public sealed record AnswerTranslateDto(Language Language, string Content);
