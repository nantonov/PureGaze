using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Answers.GetAnswersByQuestion;

public record GetAnswersByQuestionQuery(int QuestionId) : IRequest<AnswerDto?>;
