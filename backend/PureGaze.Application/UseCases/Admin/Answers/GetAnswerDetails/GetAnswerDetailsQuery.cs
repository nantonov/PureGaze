using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswerDetails;

public record GetAnswerDetailsQuery(int Id) : IRequest<AnswerDetailsDto>;
