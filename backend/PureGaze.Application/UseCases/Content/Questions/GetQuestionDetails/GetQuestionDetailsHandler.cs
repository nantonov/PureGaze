using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Content.Questions.GetQuestionDetails;

public class GetQuestionDetailsHandler(
    IQuestionRepository questionRepository, 
    IAnswerRepository answerRepository)
    : IRequestHandler<GetQuestionDetailsQuery, QuestionDetailsDto>
{
    public async Task<QuestionDetailsDto> Handle(GetQuestionDetailsQuery query, CancellationToken ct = default)
    {
        var question = await questionRepository.GetByIdAsync(query.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {query.Id} not found.");

        var answer = await answerRepository.GetByQuestionIdAsync(question.Id, ct);

        return new QuestionDetailsDto
        {
            Id = question.Id,
            SubTopicId = question.SubTopicId,
            Translates = question.QuestionTranslates.Select(t => new QuestionTranslateInfoDto
            {
                Language = t.Language,
                Content = t.Content
            }).ToList(),
            Answer = answer != null ? new AnswerDetailsDto
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                Translates = answer.AnswerTranslates.Select(t => new AnswerTranslateInfoDto
                {
                    Language = t.Language,
                    Content = t.Content
                }).ToList()
            } : null
        };
    }
}
