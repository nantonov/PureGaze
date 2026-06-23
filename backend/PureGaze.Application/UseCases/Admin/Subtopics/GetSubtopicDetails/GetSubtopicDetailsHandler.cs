using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicDetails;

public class GetSubtopicDetailsHandler(ISubtopicRepository subtopicRepository)
    : IRequestHandler<GetSubtopicDetailsQuery, GetSubtopicDetailsResult>
{
    public async Task<GetSubtopicDetailsResult> Handle(GetSubtopicDetailsQuery query, CancellationToken ct = default)
    {
        Subtopic subtopic = await subtopicRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {query.Id} not found.");

        return GetSubtopicDetailsResult.ToResult(subtopic);
    }
}
