using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Answers.GetAnswerDetails;

public record GetAnswerDetailsQuery(int Id) : IRequest<AnswerDetailsDto>;
