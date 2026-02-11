using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Questions.DeleteQuestion;

public record DeleteQuestionCommand(int Id) : IRequest;
