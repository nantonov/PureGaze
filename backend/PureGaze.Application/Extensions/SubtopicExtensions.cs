using PureGaze.Application.Contracts.Application;
using PureGaze.Application.UseCases.Content.Subtopics.CreateSubtopic;
using PureGaze.Application.UseCases.Content.Subtopics.UpdateSubtopic;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.Extensions;

public static class SubtopicExtensions
{
    extension(Subtopic subtopic)
    {
        public SubtopicDetailsDto ToDetailsDto()
            => new()
            {
                Id = subtopic.Id,
                TopicId = subtopic.TopicId,
                Translates = subtopic.SubtopicTranslates.Select(t => new SubtopicTranslateInfoDto
                {
                    Language = t.Language,
                    Name = t.Name
                }).ToList()
            };

        public void Update(IEnumerable<UpdateSubtopicTranslateDto> translates)
        {
            foreach (var translateDto in translates)
            {
                subtopic.SubtopicTranslates.SyncTranslate(
                    translateDto.Language,
                    t => t.Name = translateDto.Name,
                    lang => new SubtopicTranslate { SubtopicId = subtopic.Id, Language = lang, Name = translateDto.Name },
                    t => t.Language == translateDto.Language);
            }
        }
    }

    public static Subtopic ToEntity(this CreateSubtopicCommand command)
        => new()
        {
            TopicId = command.TopicId,
            SubtopicTranslates = command.Translates.Select(t => new SubtopicTranslate
            {
                Language = t.Language,
                Name = t.Name
            }).ToList(),
            Questions = command.Questions.Select(qDto => new Question
            {
                QuestionTranslates = qDto.Translates.Select(t => new QuestionTranslate
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList(),
                Answer = new Answer
                {
                    AnswerTranslates = qDto.Answer.Translates.Select(t => new AnswerTranslate
                    {
                        Language = t.Language,
                        Content = t.Content
                    }).ToList()
                }
            }).ToList()
        };
}
