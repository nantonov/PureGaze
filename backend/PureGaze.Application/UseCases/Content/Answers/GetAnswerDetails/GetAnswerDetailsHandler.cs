using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Answers.GetAnswerDetails;

public class GetAnswerDetailsHandler(IAnswerRepository answerRepository)
    : IRequestHandler<GetAnswerDetailsQuery, AnswerDetailsDto>
{
    public async Task<AnswerDetailsDto> Handle(GetAnswerDetailsQuery query, CancellationToken ct = default)
    {
        var answer = await answerRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Answer with Id {query.Id} not found.");

        return answer.ToDetailsDto();
    }
}
