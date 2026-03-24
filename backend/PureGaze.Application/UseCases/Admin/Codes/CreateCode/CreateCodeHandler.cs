using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;
using PureGaze.Domain.Enums;

namespace PureGaze.Application.UseCases.Admin.Codes.CreateCode;

public class CreateCodeHandler(ICodeRepository codeRepository)
    : IRequestHandler<CreateCodeCommand>
{
    public async Task Handle(CreateCodeCommand command, CancellationToken ct = default)
    {
        var code = new Code
        {
            GradeId = command.GradeId,
            ToGradeId = command.ToGradeId,
            Display = command.Display,
            TotalEx = command.TotalEx,
            DiffEx = command.DiffEx,
            CodeTranslates =
            [
                new CodeTranslate { Language = Language.Ru, LevelVision = command.LevelVisionRu },
                new CodeTranslate { Language = Language.En, LevelVision = command.LevelVisionEn }
            ]
        };

        await codeRepository.AddAsync(code, ct);
    }
}