using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Evaluation.CreateSubtopicScore;

public sealed record CreateSubtopicScoreCommand(
    int StageId, 
    int SubtopicId, 
    AssessmentMark Score, 
    string? Comment) : IRequest;