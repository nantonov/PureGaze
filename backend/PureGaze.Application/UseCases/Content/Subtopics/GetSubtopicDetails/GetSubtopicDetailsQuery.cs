using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Subtopics.GetSubtopicDetails;

public record GetSubtopicDetailsQuery(int Id) : IRequest<SubtopicDetailsDto>;
