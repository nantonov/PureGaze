using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

public sealed class GetAssessmentHistoryHandler(IAssessmentRepository assessmentRepository)
    : IRequestHandler<GetAssessmentHistoryQuery, GetAssessmentHistoryResult>
{
    public async Task<GetAssessmentHistoryResult> Handle(GetAssessmentHistoryQuery query, CancellationToken ct)
    {
        (IReadOnlyList<Domain.Entities.Assessment> items, int total) = await assessmentRepository
            .GetHistoryAssessmentsAsync(query.Search, query.Page, query.PageSize, ct);

        return new GetAssessmentHistoryResult(total, [.. items.Select(GetAssessmentHistoryDto.ToDto)]);
    }
}
