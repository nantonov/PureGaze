using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Questions.GetQuestionDetails;

public class GetQuestionDetailsHandler(IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionDetailsQuery, QuestionDetailsDto>
{
    public async Task<QuestionDetailsDto> Handle(GetQuestionDetailsQuery query, CancellationToken ct = default)
    {
        var question = await questionRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {query.Id} not found.");

        return question.ToDetailsDto();
    }
}
