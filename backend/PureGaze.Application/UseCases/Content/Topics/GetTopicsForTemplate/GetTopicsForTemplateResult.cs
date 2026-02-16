using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Topics.GetTopicsForTemplate;

public sealed record GetTopicsForTemplateResult(List<TopicDto> Topics) : IRequest;
