using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Questions.GetQuestionDetails;

public class GetQuestionDetailsHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionDetailsQuery, GetQuestionDetailsResult>
{
    public async Task<GetQuestionDetailsResult> Handle(GetQuestionDetailsQuery query, CancellationToken ct = default)
    {
        Question question = await questionRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {query.Id} not found.");

        return GetQuestionDetailsResult.ToDto(question);
    }
}
