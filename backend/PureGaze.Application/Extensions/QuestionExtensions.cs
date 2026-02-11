using PureGaze.Application.Contracts.Application;
using PureGaze.Application.UseCases.Content.Questions.CreateQuestionWithAnswer;
using PureGaze.Application.UseCases.Content.Questions.UpdateQuestion;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class QuestionExtensions
{
    public static QuestionDto ToDto(this Question question)
        => new()
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            Translates = question.QuestionTranslates.Select(t => new QuestionTranslateInfoDto
            {
                Language = t.Language,
                Content = t.Content
            }).ToList()
        };

    public static QuestionDetailsDto ToDetailsDto(this Question question)
        => new()
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            Translates = question.QuestionTranslates.Select(t => new QuestionTranslateInfoDto
            {
                Language = t.Language,
                Content = t.Content
            }).ToList(),
            Answer = question.Answer.ToDetailsDto()
        };
        
    public static void Update(this Question question, IEnumerable<UpdateQuestionTranslateDto> translates, UpdateQuestionAnswerDto answerDto)
    {
        foreach (var translateDto in translates)
        {
            question.QuestionTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Content = translateDto.Content,
                lang => new QuestionTranslate { QuestionId = question.Id, Language = lang, Content = translateDto.Content },
                t => t.Language == translateDto.Language);
        }

        foreach (var translateDto in answerDto.Translates)
        {
            question.Answer.AnswerTranslates.SyncTranslate(
                translateDto.Language,
                t => t.Content = translateDto.Content,
                lang => new AnswerTranslate { AnswerId = question.Answer.Id, Language = lang, Content = translateDto.Content },
                t => t.Language == translateDto.Language);
        }
    }

    public static Question ToEntity(this CreateQuestionWithAnswerCommand command)
        => new()
        {
            SubTopicId = command.SubTopicId,
            QuestionTranslates = command.Translates.Select(t => new QuestionTranslate
            {
                Language = t.Language,
                Content = t.Content
            }).ToList(),
            Answer = new Answer
            {
                AnswerTranslates = command.Answer.Translates.Select(t => new AnswerTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList()
            }
        };
}
