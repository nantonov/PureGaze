using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Requests;
using PureGaze.Domain.Entities;

namespace PureGaze.Application.UseCases.Admin.Codes.CreateCode;

public class CreateCodeHandler(ICodeRepository codeRepository)
    : IRequestHandler<CreateCodeCommand>
{
    public async Task Handle(CreateCodeCommand command, CancellationToken ct = default)
    {
        Code code = new Code
        {
            GradeId = command.GradeId,
            ToGradeId = command.ToGradeId,
            Name = command.Name,
            TotalEx = command.TotalEx,
            DiffEx = command.DiffEx
        };

        await codeRepository.AddAsync(code, ct);
        await codeRepository.SaveChangesAsync(ct);
    }
}
