using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Subtopics.CreateSubtopic;

public sealed record CreateSubtopicCommand(
    int TopicId,
    List<SubtopicTranslateDto> Translates,
    List<CreateQuestionDto>? Questions = null) : IRequest<CreateSubtopicResult>
{
    public static Subtopic ToEntity(CreateSubtopicCommand command)
        => new()
        {
            TopicId = command.TopicId,
            SubtopicTranslates = [..command.Translates.Select(t => new SubtopicTranslate
            {
                Language = t.Language,
                Name = t.Name
            })],
            Questions = [.. (command.Questions ?? []).Select(question => new Question
            {
                QuestionTranslates = [.. question.Translates.Select(t => new QuestionTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                })],
                Answer = new Answer
                {
                    AnswerTranslates = [.. question.Answer.Translates.Select(t => new AnswerTranslate
                    {
                        Language = t.Language,
                        Content = t.Content
                    })]
                }
            })]
        };
}

public sealed record SubtopicTranslateDto(Language Language, string Name);

public sealed record CreateQuestionDto(
    List<QuestionTranslateDto> Translates,
    CreateAnswerDto Answer);

public sealed record QuestionTranslateDto(Language Language, string Content);

public sealed record CreateAnswerDto(List<AnswerTranslateDto> Translates);

public sealed record AnswerTranslateDto(Language Language, string Content);
