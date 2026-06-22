using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.EditCode;

public class EditCodeHandler(ICodeRepository codeRepository)
    : IRequestHandler<EditCodeCommand>
{
    public async Task Handle(EditCodeCommand command, CancellationToken ct = default)
    {
        Code existing = await codeRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Code with Id {command.Id} not found.");

        existing.GradeId = command.GradeId;
        existing.ToGradeId = command.ToGradeId;
        existing.Name = command.Name;
        existing.TotalEx = command.TotalEx;
        existing.DiffEx = command.DiffEx;

        //TODO: update languages version

        await codeRepository.SaveChangesAsync(ct);
    }
}