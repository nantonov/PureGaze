using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswersByQuestion;

public class GetAnswersByQuestionHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswersByQuestionQuery, AnswerDto?>
{
    public async Task<AnswerDto?> Handle(GetAnswersByQuestionQuery query, CancellationToken ct = default)
    {
        var answer = await answerRepository.GetByQuestionIdAsync(query.QuestionId, ct);

        return answer?.ToDto();
    }
}
