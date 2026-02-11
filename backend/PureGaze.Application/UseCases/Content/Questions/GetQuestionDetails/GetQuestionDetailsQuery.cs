using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Questions.GetQuestionDetails;

public record GetQuestionDetailsQuery(int Id) : IRequest<QuestionDetailsDto>;
