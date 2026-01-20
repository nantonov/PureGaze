using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Answers.GetAnswersByQuestion;

public class GetAnswersByQuestionHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswersByQuestionQuery, AnswerDto?>
{
    public async Task<AnswerDto?> Handle(GetAnswersByQuestionQuery query, CancellationToken ct = default)
    {
        var answer = await answerRepository.GetByQuestionIdAsync(query.QuestionId, ct);

        if (answer == null)
        {
            return null;
        }

        return new AnswerDto
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Translates = answer.AnswerTranslates.Select(t => new AnswerTranslateInfoDto
            {
                Language = t.Language,
                Content = t.Content
            }).ToList()
        };
    }
}
