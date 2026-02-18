using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.CompleteAssessmentStage;

public class CompleteAssessmentStageHandler(
    IAssessmentStageRepository assessmentStageRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<CompleteAssessmentStageCommand>
{
    public async Task Handle(CompleteAssessmentStageCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdWithScoresAndTopicAsync(request.AssessmentStageId, ct)
            ?? throw new KeyNotFoundException($"Assessment Stage with Id {request.AssessmentStageId} was not found.");

        if (assessmentStage.Assessor?.Email != currentUserContextProvider.GetUserEmail())
            throw new ValidationException("Only assessor can complete assessment stage");

        if (assessmentStage.Status != StageStatus.InProgress)
            throw new ValidationException("Only in progress assessment stages can be completed");

        if (assessmentStage.Scores.Count != assessmentStage.Topic?.Subtopics.Count)
            throw new ValidationException("Manager must score all subtopics");

        assessmentStage.Status = StageStatus.Completed;
        assessmentStage.IsRecommended = request.IsRecommended;

        await assessmentStageRepository.SaveChangesAsync(ct);
    }
}
