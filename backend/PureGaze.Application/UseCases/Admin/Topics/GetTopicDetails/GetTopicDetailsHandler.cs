using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicDetails;

public class GetTopicDetailsHandler(ITopicsRepository topicsRepository)
    : IRequestHandler<GetTopicDetailsQuery, GetTopicDetailsResult>
{
    public async Task<GetTopicDetailsResult> Handle(GetTopicDetailsQuery query, CancellationToken ct = default)
    {
        Topic topic = await topicsRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Topic with Id {query.Id} not found.");

        return GetTopicDetailsResult.ToResult(topic);
    }
}
