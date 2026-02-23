using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.GetTopicTranslates;

public sealed record GetTopicTranslatesQuery(
    int TopicId,
    int Page,
    int PageSize) : IRequest<GetTopicTranslatesResult>;
