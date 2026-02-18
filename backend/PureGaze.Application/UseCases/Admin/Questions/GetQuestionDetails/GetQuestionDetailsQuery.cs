using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;

public record GetQuestionDetailsQuery(int Id) : IRequest<QuestionDetailsDto>;
