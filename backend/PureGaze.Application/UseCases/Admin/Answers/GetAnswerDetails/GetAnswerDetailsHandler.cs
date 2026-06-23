using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Answers.GetAnswerDetails;

public class GetAnswerDetailsHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswerDetailsQuery, GetAnswerDetailsResult>
{
    public async Task<GetAnswerDetailsResult> Handle(GetAnswerDetailsQuery query, CancellationToken ct = default)
    {
        Answer answer = await answerRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Answer with Id {query.Id} not found.");

        return GetAnswerDetailsResult.ToResult(answer);
    }
}
