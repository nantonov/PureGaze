using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Evaluation.GetNewAssessments;

public sealed class GetNewAssessmentsHandler(
    IAssessmentRepository assessmentRepository,
    IEmployeeRepository employeeRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetNewAssessmentsQuery, GetNewAssessmentsResult>
{
    public async Task<GetNewAssessmentsResult> Handle(GetNewAssessmentsQuery query, CancellationToken ct)
    {
        string email = currentUserContextProvider.GetUserEmail();
        Employee? currentUser = await employeeRepository.GetByEmailAsync(email, ct);

        if (currentUser?.ProfessionalLevel?.OrderValue is not { } levelOrder)
            return new GetNewAssessmentsResult([]);

        IReadOnlyList<Assessment> items = await assessmentRepository.GetNewAssessmentsAsync(levelOrder, currentUser.Id, ct);

        return new GetNewAssessmentsResult([.. items.Select(a => GetNewAssessmentDto.ToDto(a, currentUser.Id))]);
    }
}
