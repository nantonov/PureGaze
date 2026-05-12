using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicDetails;

public sealed record GetTopicDetailsQuery(int Id) : IRequest<TopicDetailsDto>;
