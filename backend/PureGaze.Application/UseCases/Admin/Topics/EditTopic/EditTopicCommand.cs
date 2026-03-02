using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.EditTopic;

public sealed record EditTopicCommand(
    int TopicId,
    string NameRu,
    string NameEn) : IRequest;