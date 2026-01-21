using PureGaze.Application.Contracts.Application;
using PureGaze.Application.UseCases.Content.Answers.UpdateAnswer;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class AnswerExtensions
{
    extension(Answer answer)
    {
        public AnswerDto ToDto()
            => new()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                Translates = answer.AnswerTranslates.Select(t => new AnswerTranslateInfoDto
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList()
            };
        public AnswerDetailsDto ToDetailsDto()
            => new()
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                Translates = answer.AnswerTranslates.Select(t => new AnswerTranslateInfoDto
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList()
            };
        public void Update(IEnumerable<UpdateAnswerTranslateDto> translates)
        {
            foreach (var translateDto in translates)
            {
                answer.AnswerTranslates.SyncTranslate(
                    translateDto.Language,
                    t => t.Content = translateDto.Content,
                    lang => new AnswerTranslate { AnswerId = answer.Id, Language = lang, Content = translateDto.Content },
                    t => t.Language == translateDto.Language);
            }
        }
    }
}
