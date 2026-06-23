using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedAssessmentStages;

public sealed class GetAssignedAssessmentStagesHandler(
    IAssessmentStageRepository repository,
    ICurrentUserContextProvider currentUser)
    : IRequestHandler<GetAssignedAssessmentStagesQuery, GetAssignedAssessmentStagesResult>
{
    public async Task<GetAssignedAssessmentStagesResult> Handle(GetAssignedAssessmentStagesQuery request, CancellationToken ct)
    {
        var stages = await repository.GetAssignedToAsync(currentUser.GetUserEmail(), ct);
        return new GetAssignedAssessmentStagesResult(stages.Select(AssignedAssessmentStageDto.FromEntity).ToList());
    }
}
