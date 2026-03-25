using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Codes.EditCode;

public class EditCodeHandler(ICodeRepository codeRepository)
    : IRequestHandler<EditCodeCommand>
{
    public async Task Handle(EditCodeCommand command, CancellationToken ct = default)
    {
        var existing = await codeRepository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Code with Id {command.Id} not found.");

        existing.GradeId = command.GradeId;
        existing.ToGradeId = command.ToGradeId;
        existing.Display = command.Display;
        existing.TotalEx = command.TotalEx;
        existing.DiffEx = command.DiffEx;

        var ruTranslate = existing.CodeTranslates.FirstOrDefault(t => t.Language == Language.Ru);
        ruTranslate?.LevelVision = command.LevelVisionRu;

        var enTranslate = existing.CodeTranslates.FirstOrDefault(t => t.Language == Language.En);
        enTranslate?.LevelVision = command.LevelVisionEn;

        await codeRepository.SaveChangesAsync(ct);
    }
}