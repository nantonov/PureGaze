using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.EditTopicTranslate;

public sealed record EditTopicTranslateCommand(
    int TopicId,
    Language Language,
    string? Name) : IRequest;