using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetMyAssessmentRequests;

public sealed record GetMyAssessmentRequestsQuery(int Page, int PageSize) 
    : IRequest<GetMyAssessmentRequestsResult>;