using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Abstractions.Providers;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetNewAssessments;

public sealed class GetNewAssessmentsHandler(
    IAssessmentRepository assessmentRepository,
    IEmployeeRepository employeeRepository,
    ICurrentUserContextProvider currentUserContextProvider)
    : IRequestHandler<GetNewAssessmentsQuery, GetNewAssessmentsResult>
{
    public async Task<GetNewAssessmentsResult> Handle(GetNewAssessmentsQuery query, CancellationToken ct)
    {
        var email = currentUserContextProvider.GetUserEmail();
        var currentUser = await employeeRepository.GetByEmailAsync(email, ct);

        if (currentUser?.ProfessionalLevel?.OrderValue is not { } levelOrder)
            return new GetNewAssessmentsResult([]);

        var items = await assessmentRepository.GetNewAssessmentsAsync(levelOrder, currentUser.Id, ct);

        var dtos = items.Select(a => new AssessmentListItemDto
        {
            Id = a.Id,
            EmployeeFullName = $"{a.Employee?.FirstNameEn} {a.Employee?.LastNameEn}".Trim(),
            EmployeeEmail = a.Employee?.Email ?? string.Empty,
            GradeRange = $"{a.Code?.Grade?.Translation} -> {a.Code?.ToGrade?.Translation}",
            IsOwnAssessment = a.EmployeeId == currentUser.Id,
            Status = a.Status.ToString(),
            Stages = a.Stages.Select(s => new AssessmentStageItemDto
            {
                Id = s.Id,
                TopicName = s.Topic?.TopicTranslates.OrderBy(t => t.Language).FirstOrDefault()?.Name ?? string.Empty,
                AssessorFullName = s.Assessor is null ? null : $"{s.Assessor.FirstNameEn} {s.Assessor.LastNameEn}".Trim(),
                IsAssignedToCurrentUser = s.AssessorId == currentUser.Id
            }).ToList()
        }).ToList();

        return new GetNewAssessmentsResult(dtos);
    }
}
