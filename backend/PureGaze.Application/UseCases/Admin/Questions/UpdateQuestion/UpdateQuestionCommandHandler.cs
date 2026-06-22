using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Questions.UpdateQuestion;

public class UpdateQuestionCommandHandler(IQuestionRepository questionRepository)
    : IRequestHandler<UpdateQuestionCommand>
{
    public async Task Handle(UpdateQuestionCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        Question question = await questionRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Question with Id {command.Id} not found.");

        UpdateQuestionCommand.Update(question, command);

        await questionRepository.SaveChangesAsync(ct);
    }

    private void ValidateInput(UpdateQuestionCommand command)
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
