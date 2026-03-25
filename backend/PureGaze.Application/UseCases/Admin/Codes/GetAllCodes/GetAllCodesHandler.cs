using PureGaze.Application.Abstractions.Infrastructure;
using PureGaze.Application.Contracts.Application;
using PureGaze.Application.Extensions;
using PureGaze.Application.Requests;

namespace PureGaze.Application.UseCases.Admin.Codes.GetAllCodes;

public class GetAllCodesHandler(ICodeRepository codeRepository)
    : IRequestHandler<GetAllCodesQuery, IReadOnlyList<CodeDto>>
{
    public async Task<IReadOnlyList<CodeDto>> Handle(GetAllCodesQuery query, CancellationToken ct = default)
    {
        var codes = await codeRepository.GetAllAsync(ct);

        return [.. codes.Select(c => c.ToDto())];
    }
}