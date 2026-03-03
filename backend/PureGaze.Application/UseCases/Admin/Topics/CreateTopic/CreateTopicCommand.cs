using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Topics.CreateTopic;

public sealed record CreateTopicCommand(
    int TemplateId,
    string NameRu,
    string NameEn) : IRequest<CreateTopicResult>;
