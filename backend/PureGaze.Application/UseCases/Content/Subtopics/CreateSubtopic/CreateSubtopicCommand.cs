using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Content.Subtopics.CreateSubtopic;

public record CreateSubtopicCommand(
    int TopicId,
    List<SubtopicTranslateDto> Translates,
    List<CreateQuestionDto> Questions) : IRequest;

public record SubtopicTranslateDto(Language Language, string Name);

public record CreateQuestionDto(
    List<QuestionTranslateDto> Translates,
    CreateAnswerDto Answer);

public record QuestionTranslateDto(Language Language, string Content);

public record CreateAnswerDto(List<AnswerTranslateDto> Translates);

public record AnswerTranslateDto(Language Language, string Content);
