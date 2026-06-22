using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using Microsoft.EntityFrameworkCore;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionsBySubtopic;

public class GetQuestionsBySubtopicHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionsBySubtopicQuery, List<GetQuestionsBySubtopicResult>>
{
    public async Task<List<GetQuestionsBySubtopicResult>> Handle(GetQuestionsBySubtopicQuery query, CancellationToken ct = default)
    {
        IReadOnlyList<Question> questions = await questionRepository.GetBySubTopicIdAsync(query.SubTopicId, ct);

        return [.. questions.Select(GetQuestionsBySubtopicResult.ToResult)];
    }
}
