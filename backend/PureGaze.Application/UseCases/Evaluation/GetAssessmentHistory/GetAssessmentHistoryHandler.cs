using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

public sealed class GetAssessmentHistoryHandler(IAssessmentRepository assessmentRepository)
    : IRequestHandler<GetAssessmentHistoryQuery, GetAssessmentHistoryResult>
{
    public async Task<GetAssessmentHistoryResult> Handle(GetAssessmentHistoryQuery query, CancellationToken ct)
    {
        var (items, total) = await assessmentRepository
            .GetHistoryAssessmentsAsync(query.Search, query.Page, query.PageSize, ct);

        var dtos = items.Select(a => new AssessmentHistoryItemDto
        {
            Id = a.Id,
            EmployeeFullName = $"{a.Employee?.FirstNameEn} {a.Employee?.LastNameEn}".Trim(),
            EmployeeEmail = a.Employee?.Email ?? string.Empty,
            GradeRange = $"{a.Code?.Grade?.Translation} -> {a.Code?.ToGrade?.Translation}",
            Status = a.Status.ToString(),
            CreatedAt = a.CreatedAt
        }).ToList();

        return new GetAssessmentHistoryResult(total, dtos);
    }
}
