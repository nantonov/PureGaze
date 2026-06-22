using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;

public record GetQuestionsBySubtopicQuery(int SubTopicId) : IRequest<List<GetQuestionsBySubtopicResult>>;
