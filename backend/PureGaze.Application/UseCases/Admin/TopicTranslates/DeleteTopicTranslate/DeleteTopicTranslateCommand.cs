using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.TopicTranslates.DeleteTopicTranslate;

public sealed record DeleteTopicTranslateCommand(
    int TopicId,
    Language Language) : IRequest;
