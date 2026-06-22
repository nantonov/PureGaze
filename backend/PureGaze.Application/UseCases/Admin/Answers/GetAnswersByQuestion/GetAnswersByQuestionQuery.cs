using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswersByQuestion;

public record GetAnswersByQuestionQuery(int QuestionId) : IRequest<GetAnswersByQuestionResult?>;
