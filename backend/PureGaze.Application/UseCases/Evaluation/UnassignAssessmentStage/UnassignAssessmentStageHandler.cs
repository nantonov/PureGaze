using System.ComponentModel.DataAnnotations;
using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.UnassignAssessmentStage;

public sealed class UnassignAssessmentStageHandler(
    IAssessmentStageRepository assessmentStageRepository,
    IEmployeeRepository employeeRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<UnassignAssessmentStageCommand>
{
    public async Task Handle(UnassignAssessmentStageCommand request, CancellationToken ct)
    {
        AssessmentStage stage = await assessmentStageRepository.GetByIdAsync(request.AssessmentStageId, ct)
            ?? throw new KeyNotFoundException($"Assessment stage with Id {request.AssessmentStageId} not found.");

        string email = currentUserContextProvider.GetUserEmail();
        Employee currentUser = await employeeRepository.GetByEmailAsync(email, ct)
            ?? throw new KeyNotFoundException($"Employee with email {email} not found.");

        if (stage.AssessorId != currentUser.Id)
            throw new ValidationException("You can only unassign yourself from an assessment stage.");

        stage.AssessorId = null;
        await assessmentStageRepository.SaveChangesAsync(ct);
    }
}
