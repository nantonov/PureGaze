using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Subtopics.GetSubtopicDetails;

public sealed record GetSubtopicDetailsQuery(int Id) : IRequest<SubtopicDetailsDto>;
