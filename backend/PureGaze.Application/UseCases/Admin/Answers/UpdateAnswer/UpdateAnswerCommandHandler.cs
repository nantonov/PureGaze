using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Answers.UpdateAnswer;

public class UpdateAnswerCommandHandler(IAnswerRepository answerRepository)
    : IRequestHandler<UpdateAnswerCommand>
{
    public async Task Handle(UpdateAnswerCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        Answer answer = await answerRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Answer with Id {command.Id} not found.");

        UpdateAnswerCommand.Apply(answer, command);

        await answerRepository.SaveChangesAsync(ct);
    }

    private static void ValidateInput(UpdateAnswerCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one answer translate is required.");
    }
}
