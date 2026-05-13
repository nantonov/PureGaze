using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicDetails;

public class GetTopicDetailsHandler(ITopicsRepository topicsRepository)
    : IRequestHandler<GetTopicDetailsQuery, TopicDetailsDto>
{
    public async Task<TopicDetailsDto> Handle(GetTopicDetailsQuery query, CancellationToken ct = default)
    {
        var topic = await topicsRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Topic with Id {query.Id} not found.");

        return topic.ToDetailsDto();
    }
}
