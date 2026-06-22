using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswersByQuestion;

public class GetAnswersByQuestionHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswersByQuestionQuery, GetAnswersByQuestionResult?>
{
    public async Task<GetAnswersByQuestionResult?> Handle(GetAnswersByQuestionQuery query, CancellationToken ct = default)
    {
        Answer? answer = await answerRepository.GetByQuestionIdAsync(query.QuestionId, ct);

        return answer is null ? null : GetAnswersByQuestionResult.ToResult(answer);
    }
}
