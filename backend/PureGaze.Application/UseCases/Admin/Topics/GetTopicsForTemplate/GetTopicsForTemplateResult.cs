using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.GetTopicsForTemplate;

public sealed record GetTopicsForTemplateResult(List<TopicDto> Topics) : IRequest;
