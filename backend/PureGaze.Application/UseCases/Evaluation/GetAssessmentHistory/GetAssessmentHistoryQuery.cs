using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssessmentHistory;

public sealed record GetAssessmentHistoryQuery(int Page, int PageSize, string? Search)
    : IRequest<GetAssessmentHistoryResult>;
