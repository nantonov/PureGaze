using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Answers.UpdateAnswer;

public class UpdateAnswerCommandHandler(IAnswerRepository answerRepository)
    : IRequestHandler<UpdateAnswerCommand>
{
    public async Task Handle(UpdateAnswerCommand command, CancellationToken ct = default)
    {
        ValidateInput(command);

        var answer = await answerRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Answer with Id {command.Id} not found.");

        answer.Update(command.Translates);

        await answerRepository.SaveChangesAsync(ct);
    }

    private static void ValidateInput(UpdateAnswerCommand command)
    {
        ArgumentNullException.ThrowIfNull(command.Translates);

        if (command.Translates.Count == 0)
            throw new ArgumentException("At least one answer translate is required.");
    }
}
