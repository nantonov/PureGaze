using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Questions.CreateQuestionWithAnswer;

public class CreateQuestionWithAnswerCommandHandler(
    IQuestionRepository questionRepository,
    ISubtopicRepository subtopicRepository)
    : IRequestHandler<CreateQuestionWithAnswerCommand, int>
{
    public async Task<int> Handle(CreateQuestionWithAnswerCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        _ = await subtopicRepository.GetByIdAsync(command.SubTopicId, ct)
            ?? throw new KeyNotFoundException($"Subtopic with Id {command.SubTopicId} not found.");

        Question question = CreateQuestionWithAnswerCommand.ToEntity(command);

        await questionRepository.AddAsync(question, ct);
        await questionRepository.SaveChangesAsync(ct);

        return question.Id;
    }

    private void ValidateInput(CreateQuestionWithAnswerCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);
        ArgumentNullException.ThrowIfNull(command.Answer);
        ArgumentNullException.ThrowIfNull(command.Answer.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one question translate is required.");
        
        if (command.Answer.Translates.Count == 0)
            throw new ArgumentException("At least one answer translate is required.");
    }
}
