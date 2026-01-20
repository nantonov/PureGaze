using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;
using Microsoft.EntityFrameworkCore;

namespace PureGaze.Application.UseCases.Content.Questions.GetQuestionsBySubtopic;

public class GetQuestionsBySubtopicHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionsBySubtopicQuery, List<QuestionDto>>
{
    public async Task<List<QuestionDto>> Handle(GetQuestionsBySubtopicQuery query, CancellationToken ct = default)
    {
        var questions = await questionRepository.GetBySubTopicIdAsync(query.SubTopicId, ct);

        return questions.Select(q => new QuestionDto
        {
            Id = q.Id,
            SubTopicId = q.SubTopicId,
            Translates = q.QuestionTranslates.Select(t => new QuestionTranslateInfoDto
            {
                Language = t.Language,
                Content = t.Content
            }).ToList()
        }).ToList();
    }
}
