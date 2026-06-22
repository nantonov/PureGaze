using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Subtopics.UpdateSubtopic;

public sealed record UpdateSubtopicCommand(
    int Id,
    List<UpdateSubtopicTranslateDto> Translates) : IRequest
{
    public static void Apply(Subtopic subtopic, UpdateSubtopicCommand command)
    {
        foreach (UpdateSubtopicTranslateDto translate in command.Translates)
        {
            TranslationSync.Update(
                subtopic.SubtopicTranslates,
                translate.Language,
                current => current.Name = translate.Name,
                language => new SubtopicTranslate
                {
                    SubtopicId = subtopic.Id,
                    Language = language,
                    Name = translate.Name
                },
                current => current.Language == translate.Language);
        }
    }
}

public sealed record UpdateSubtopicTranslateDto(Language Language, string Name);
