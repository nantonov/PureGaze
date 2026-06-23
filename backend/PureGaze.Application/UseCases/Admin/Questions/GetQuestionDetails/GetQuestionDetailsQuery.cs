using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;

public record GetQuestionDetailsQuery(int Id) : IRequest<GetQuestionDetailsResult>;
