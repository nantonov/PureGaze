using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetAssignedToMeRequests;

public sealed record GetAssignedToMeRequestsQuery(int Page, int PageSize) 
    : IRequest<GetAssignedToMeRequestsResult>;