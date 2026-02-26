using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.ScoreSubtopic;

public sealed record ScoreSubtopicCommand(
    int StageId,
    int SubtopicId,
    AssessmentMark Score,
    string? Comment) : IRequest;