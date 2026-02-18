using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicDetails;

public class GetSubtopicDetailsHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<GetSubtopicDetailsQuery, SubtopicDetailsDto>
{
    public async Task<SubtopicDetailsDto> Handle(GetSubtopicDetailsQuery query, CancellationToken ct = default)
    {
        var subtopic = await subtopicRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {query.Id} not found.");

        return subtopic.ToDetailsDto();
    }
}
