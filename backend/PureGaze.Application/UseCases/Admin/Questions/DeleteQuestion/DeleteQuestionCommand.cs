using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Questions.DeleteQuestion;

public record DeleteQuestionCommand(int Id) : IRequest;
