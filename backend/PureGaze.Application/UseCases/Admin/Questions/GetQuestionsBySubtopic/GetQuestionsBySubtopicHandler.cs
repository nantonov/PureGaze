using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;

public class GetQuestionsBySubtopicHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionsBySubtopicQuery, List<QuestionDto>>
{
    public async Task<List<QuestionDto>> Handle(GetQuestionsBySubtopicQuery query, CancellationToken ct = default)
    {
        var questions = await questionRepository.GetBySubTopicIdAsync(query.SubTopicId, ct);

        return questions.Select(q => q.ToDto()).ToList();
    }
}
