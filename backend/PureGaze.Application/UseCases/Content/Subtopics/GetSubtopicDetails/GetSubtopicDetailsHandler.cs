using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Subtopics.GetSubtopicDetails;

public class GetSubtopicDetailsHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<GetSubtopicDetailsQuery, SubtopicDetailsDto>
{
    public async Task<SubtopicDetailsDto> Handle(GetSubtopicDetailsQuery query, CancellationToken ct = default)
    {
        var subtopic = await subtopicRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {query.Id} not found.");

        return new SubtopicDetailsDto
        {
            Id = subtopic.Id,
            TopicId = subtopic.TopicId,
            Translates = subtopic.SubtopicTranslates.Select(t => new SubtopicTranslateInfoDto
            {
                Language = t.Language,
                Name = t.Name
            }).ToList()
        };
    }
}
