using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Evaluation.GetNewAssessments;

public sealed record GetNewAssessmentsQuery : IRequest<GetNewAssessmentsResult>;
