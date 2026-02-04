using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Questions.GetQuestionsBySubtopic;

public record GetQuestionsBySubtopicQuery(int SubTopicId) : IRequest<List<QuestionDto>>;
