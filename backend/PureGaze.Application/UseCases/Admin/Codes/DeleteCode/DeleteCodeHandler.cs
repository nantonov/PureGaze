using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.DeleteCode;

public class DeleteCodeHandler(ICodeRepository codeRepository)
    : IRequestHandler<DeleteCodeCommand>
{
    public async Task Handle(DeleteCodeCommand command, CancellationToken ct = default)
    {
        var code = await codeRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Code with Id {command.Id} not found.");

        codeRepository.Delete(code);
        await codeRepository.SaveChangesAsync(ct);
    }
}