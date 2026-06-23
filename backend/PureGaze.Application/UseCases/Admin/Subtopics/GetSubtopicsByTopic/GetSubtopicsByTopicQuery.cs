using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicsByTopic;

public sealed record GetSubtopicsByTopicQuery(int TopicId)
    : IRequest<List<GetSubtopicsByTopicResult>>;
