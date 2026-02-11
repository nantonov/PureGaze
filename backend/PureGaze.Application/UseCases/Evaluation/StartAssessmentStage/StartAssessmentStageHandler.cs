using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PureGaze.Application.UseCases.Evaluation.StartAssessmentStage;

public class StartAssessmentStageHandler(
    IAssessmentStageRepository assessmentStageRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<StartAssessmentStageCommand>
{
    public async Task Handle(StartAssessmentStageCommand request, CancellationToken ct)
    {
        var assessmentStage = await assessmentStageRepository.GetByIdAsync(request.AssessmentStageId, ct)
            ?? throw new KeyNotFoundException($"Assessment Stage with Id {request.AssessmentStageId} was not found.");

        if (assessmentStage.Assessor?.Email != currentUserContextProvider.GetUserEmail())
            throw new ValidationException("Only assessor can start assessment stage");

        if (assessmentStage.Status != StageStatus.Pending)
            throw new ValidationException("Only pending assesment stage could be started");

        assessmentStage.Status = StageStatus.InProgress;

        await assessmentStageRepository.SaveChangesAsync(ct);
    }
}
