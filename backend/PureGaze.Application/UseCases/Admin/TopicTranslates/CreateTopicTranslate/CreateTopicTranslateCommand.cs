using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.CreateTopicTranslate;

public sealed record CreateTopicTranslateCommand(
    int TopicId,
    Language Language,
    string? Name) : IRequest;